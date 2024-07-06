using authServer.Data;
using authServer.Exceptions;
using authServer.Options;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace authServer.Services.DeviceService;

public interface IDeviceVerificationService
{
    public Task VerifyDeviceFromMailLink(Int64 deviceId, string verificationToken);
    public Task InitVerifyDeviceUser(User user, List<IdDto> devices);
    public Task<List<DeviceDto>> VerifyDeviceByAdmin(User admin, Int64 userId, List<IdDto> devices);
}

public class DeviceVerificationService(
    AuthDataContext authDataContext,
    IOptions<JwtOptions> jwtOptions,
    IMailerVerificationService mailerVerificationService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    ITokenWriteService tokenWriteService,
    IMapper mapper
) : IDeviceVerificationService
{
    public async Task VerifyDeviceFromMailLink(Int64 deviceId, string verificationToken)
    {
        if (!tokenValidationService.IsValidDeviceVerificationToken(verificationToken))
        {
            throw new TokenInvalidException("invalidAuthorizationKey");
        }

        if (authDataContext.BlockedDeviceVerifyTokens.Any(t => t.Token == verificationToken))
        {
            throw new BlockedTokenException("sessionExpired");
        }
        Int64 userId = tokenReadService.GetUserId(verificationToken);
        User? dbUser = await authDataContext
            .Users.Include(u => u.Devices)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (dbUser == null)
        {
            throw new TokenDataException("dataInvalidAuthorizationKey");
        }

        var device = dbUser.Devices.FirstOrDefault(d => d.Id == deviceId);
        if (device == null)
        {
            throw new DeviceDoesNotExistException("providedDeviceHasNotBeenFound");
        }

        if (device.Verified)
        {
            throw new AlreadyVerifiedException("youHaveAlreadyVerifiedYourDevice");
        }

        device.Verified = true;
        dbUser.VerifyDeviceDateTime = null;
        authDataContext.BlockedDeviceVerifyTokens.Add(
            new BlockedDeviceVerifyToken { Token = verificationToken, DateTime = DateTime.Now }
        );
        await authDataContext.SaveChangesAsync();
    }

    public async Task InitVerifyDeviceUser(User user, List<IdDto> devices)
    {
        if (devices.Count > 1)
        {
            throw new BadHttpRequestException("selectOnlyOneDevice");
        }
        foreach (var device in devices)
        {
            var dbDevice = user.Devices.FirstOrDefault(d => d.Id == device.Id);
            if (dbDevice == null)
            {
                throw new ForeignDeviceException("deviceNotBelongingToAccount");
            }

            if (dbDevice.Verified)
            {
                throw new AlreadyVerifiedException("deviceAlreadyVerified");
            }
            if (
                user.VerifyDeviceDateTime == null
                || (
                    user.VerifyDeviceDateTime != null
                    && (DateTime.Now - user.VerifyDeviceDateTime).Value.TotalMinutes
                        > jwtOptions.Value.DeviceVerificationTokenExpiryMinutes
                )
            )
            {
                tokenWriteService.WriteDeviceVerificationToken(user, out string dbToken);
                user.VerifyDeviceDateTime = DateTime.Now;
                await authDataContext.SaveChangesAsync();
                _ = Task.Run(() =>
                {
                    mailerVerificationService.SendDeviceVerificationEmail(user, device.Id, dbToken);
                });
                throw new NotifyingException("newDeviceVerificationEmailHasBeenSent");
            }
            throw new NotifyingException(
                "youNeedToVerifyDeviceWithEmailCheckEmailOrWait15minutesAndRetry"
            );
        }
    }

    public async Task<List<DeviceDto>> VerifyDeviceByAdmin(
        User admin,
        Int64 userId,
        List<IdDto> devices
    )
    {
        var user = admin.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new ForeignUserException("userDoesNotBelongToTheAccount");
        }
        if (devices.Count > 1)
        {
            throw new BadHttpRequestException("selectOnlyOneDevice");
        }

        var updatedDevices = new List<Device>();
        foreach (var device in devices)
        {
            var dbDevice =
                user.Devices.FirstOrDefault(d => d.Id == device.Id)
                ?? throw new ForeignDeviceException("deviceNotBelongingToAccount");

            if (dbDevice.Verified)
                throw new AlreadyVerifiedException("deviceAlreadyVerified");

            dbDevice.Verified = true;
            await authDataContext.SaveChangesAsync();
            updatedDevices.Add(dbDevice);
        }

        return updatedDevices.Select(mapper.Map<DeviceDto>).ToList();
    }
}
