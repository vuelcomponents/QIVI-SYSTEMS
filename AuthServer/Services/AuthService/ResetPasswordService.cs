using authServer.Attributes;
using authServer.Exceptions;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.LicenceRepository;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Models;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.AuthService;

public interface IResetPasswordService
{
    bool VerifyResetPasswordKey(string token, out User? user);
    void ResetPasswordByKey(string t, string newPassword);
    void InitResetPassword(string email, bool firstPassword, out string rToken, bool manual);
}

public class ResetPasswordService(
    IUserRepository userRepository,
    IBlockedTokenRepository blockedTokenRepository,
    ITokenWriteService tokenWriteService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    IMailerResetPasswordService mailerResetPasswordService,
    IHashService hashService,
    IOptions<JwtOptions> jwtOptions
) : IResetPasswordService
{
    public void InitResetPassword(string email, bool firstPassword, out string rToken, bool manual)
    {
        User? user = userRepository.Get(u=> u.Email.Equals(email));
        if (user == null)
        {
            throw new UserDoesNotExistException("userDoesNotExist");
        }
        if (user.Verified is null or false)
        {
            throw new NotifyingException("userIsNotVerified");
        }

        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -jwtOptions.Value.ResetPasswordTokenExpiryMinutes
        );
        if (user.ResetPasswordDateTime > expiryTimeAgo)
        {
            throw new NotifyingException(
                $"pleaseCheckYourEmailForResetPasswordOrRetryAfter15minutes"
            );
        }

        // Write Reset Token And Sign Reset DateTime
        tokenWriteService.WriteResetPasswordToken(user, out string token);
        rToken = token;
        user.ResetPasswordDateTime = DateTime.Now;
        userRepository.Save();

        _ = Task.Run(() =>
        {
            mailerResetPasswordService.SendResetPassword(user, token, firstPassword);
        });

        if (manual)
            throw new NotifyingException($"visitMailBoxAndConfirmPasswordReset");
    }

    public bool VerifyResetPasswordKey(string token, out User? user)
    {
        if (
            (
                blockedTokenRepository.Has(t =>
                    t.Token == token && t.Discriminator == "BlockedResetPasswordToken"
                )
            )
        )
        {
            throw new BlockedTokenException($"sessionExpired");
        }
        var tokenD = Uri.UnescapeDataString(token);
        if (!tokenValidationService.IsValidResetPasswordToken(tokenD))
        {
            throw new TokenInvalidException($"invalidAuthorizationKey");
        }

        var userId = tokenReadService.GetUserId(tokenD);
        user = userRepository.Get(u=>u.Id.Equals(userId));
        if (user == null)
        {
            throw new TokenDataException("dataInvalidAuthorizationKey");
        }
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -jwtOptions.Value.ResetPasswordTokenExpiryMinutes
        );
        if (user.ResetPasswordDateTime == null || user.ResetPasswordDateTime < expiryTimeAgo)
        {
            throw new TokenExpiredException($"yourResetPasswordHasExpired");
        }
        return true;
    }

    public void ResetPasswordByKey(string t, string newPassword)
    {
        VerifyResetPasswordKey(t, out User? user);
        hashService.Hash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
        user!.Password = passwordHash;
        user.Salt = passwordSalt;
        user.ResetPasswordDateTime = null;
        var blockedResetPasswordToken = new BlockedResetPasswordToken
        {
            Token = t,
            DateTime = DateTime.Now
        };
        blockedTokenRepository.Create(
            blockedResetPasswordToken,
            "BlockedResetPasswordToken"
        );
        blockedTokenRepository.Save();
    }
}
