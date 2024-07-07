using System.Text;
using authServer.Exceptions;
using authServer.Options;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.AuthService;

public interface ILoginService
{
    Task<AuthResponse> Login(EmailPasswordDto user, string ip, string userAgent);
    Task HandleUnverifiedUser(User dbUser);
}

public class LoginService(
    IUserRepository userRepository,
    ITokenWriteService tokenWriteService,
    IMailerVerificationService mailerVerificationService,
    IHashService hashService,
    IOptions<JwtOptions> jwtOptions,
    IMapper mapper
) : ILoginService
{
    public async Task<AuthResponse> Login(EmailPasswordDto user, string ip, string userAgent)
    {
        if (user.Email.IsNullOrEmpty())
            throw new IncorrectFormDataException("youMustFillAnEmail");
        if (user.Password.IsNullOrEmpty())
            throw new IncorrectFormDataException("youMustFillAPassword");

        User? dbUser = await userRepository.GetAndIncludeDeviceWithTokenAsync(
            u=> u.Email.Equals(user.Email!)
        );

        if (dbUser == null)
            throw new IncorrectFormDataException("userWithThisEmailDoesNotExist");

        if (dbUser.Verified is null or false)
            await HandleUnverifiedUser(dbUser);

        if (!hashService.VerifyHash(user.Password!, dbUser.Password, dbUser.Salt))
        {
            throw new IncorrectFormDataException("incorrectLoginData");
        }

        if (dbUser.Blocked)
        {
            throw new AccountBlockedException("accountBlocked");
        }

        tokenWriteService.WriteToken(dbUser, out string token, out string refreshToken);

        await HandleDeviceAndIpAccessibility(dbUser, ip, userAgent, token);
        dbUser.MonthlyVisits++;
        dbUser.MonthlyActivities++;
        dbUser.RefreshToken = Encoding.UTF8.GetBytes(refreshToken);
        await userRepository.SaveAsync();

        AuthResponse authResponse = new AuthResponse
        {
            User = mapper.Map<UserDto>(dbUser),
            Token = token
        };
        return authResponse;
    }

    public async Task HandleUnverifiedUser(User dbUser)
    {
        if (dbUser.Verified is null or false)
        {
            bool isVerifyEmailTimeNullOrPreviousSessionExpired =
                dbUser.VerifyEmailDateTime != null
                && (DateTime.Now - dbUser.VerifyEmailDateTime).Value.TotalMinutes
                    > jwtOptions.Value.VerificationTokenExpiryMinutes;
            if (isVerifyEmailTimeNullOrPreviousSessionExpired)
            {
                dbUser.VerifyEmailDateTime = DateTime.Now;
                await userRepository.SaveAsync();
                _ = Task.Run(() =>
                {
                    tokenWriteService.WriteVerificationToken(dbUser, out string vToken);
                    mailerVerificationService.SendVerificationEmail(dbUser, vToken);
                });
                throw new NotifyingException("newVerificationEmailHasBeenSent");
            }

            throw new NotifyingException("youNeedToVerifyEmail");
        }
    }

    private async Task HandleDeviceAndIpAccessibility(
        User dbUser,
        string ip,
        string userAgent,
        string token
    )
    {
        var dbIp = dbUser.Devices.FirstOrDefault(d => d.Ip == ip && d.IpBlocked);
        if (dbIp != null)
            throw new IpBlockedException("ipHasBeenBlocked");

        var device = dbUser.Devices.FirstOrDefault(d => d.Ip == ip && d.UserAgent == userAgent);

        if (device == null)
        {
            var newDevice = new Device
            {
                Ip = ip,
                UserAgent = userAgent,
                Token = new SessionToken { DateTime = DateTime.Now, Token = token }
            };
            dbUser.Devices.Add(newDevice);
            newDevice.SignedInDateTime = DateTime.Now;

            await userRepository.SaveAsync();

            if (dbUser.ClaimDeviceVerification)
            {
                tokenWriteService.WriteDeviceVerificationToken(dbUser, out string dbToken);
                dbUser.VerifyDeviceDateTime = DateTime.Now;
                await userRepository.SaveAsync();
                _ = Task.Run(() =>
                {
                    mailerVerificationService.SendDeviceVerificationEmail(
                        dbUser,
                        newDevice.Id,
                        dbToken
                    );
                });
                throw new NotifyingException("pleaseConfirmYourDeviceWithEmail");
            }
        }
        else if (device.Blocked)
        {
            throw new DeviceBlockedException("thisDeviceHasBeenBlocked");
        }
        else if (dbUser.ClaimDeviceVerification && !device.Verified)
        {
            bool isVerifyDeviceTimeNullOrPreviousSessionExpired =
                dbUser.VerifyDeviceDateTime == null
                || (
                    dbUser.VerifyDeviceDateTime != null
                    && (DateTime.Now - dbUser.VerifyDeviceDateTime).Value.TotalMinutes
                        > jwtOptions.Value.DeviceVerificationTokenExpiryMinutes
                );

            if (isVerifyDeviceTimeNullOrPreviousSessionExpired)
            {
                await VerificationRetry();
            }

            throw new NotifyingException(
                "youNeedToVerifyDeviceWithEmailCheckEmailOrWait15minutesAndRetry"
            );
        }
        else
        {
            device.Token = new SessionToken { DateTime = DateTime.Now, Token = token };
            device.SignedInDateTime = DateTime.Now;
        }

        async Task VerificationRetry()
        {
            tokenWriteService.WriteDeviceVerificationToken(dbUser, out string dbToken);
            dbUser.VerifyDeviceDateTime = DateTime.Now;
            await userRepository.SaveAsync();
            _ = Task.Run(() =>
            {
                mailerVerificationService.SendDeviceVerificationEmail(dbUser, device.Id, dbToken);
            });
            throw new NotifyingException("newDeviceVerificationEmailHasBeenSent");
        }
    }
}
