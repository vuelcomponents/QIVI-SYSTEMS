using authServer.Exceptions;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.AuthService;

public interface IChangeEmailService
{
    Task InitChangeEmail(User user, ChangeEmailDto changeEmail);
    void ChangeEmailKey(string ceToken);
}

public class ChangeEmailService(
    IUserRepository userRepository,
    IBlockedTokenRepository blockedTokenRepository,
    ITokenWriteService tokenWriteService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    IMailerChangeEmailService mailerChangeEmailService,
    IHashService hashService,
    IOptions<JwtOptions> jwtOptions
) : IChangeEmailService
{
    public async Task InitChangeEmail(User user, ChangeEmailDto changeEmail)
    {
        if (!hashService.VerifyHash(changeEmail.Password, user.Password, user.Salt))
        {
            throw new IncorrectFormDataException("incorrectPassword");
        }

        if (user.Email == changeEmail.NewEmail)
        {
            throw new ValueRepeatException("emailsAreEqual");
        }
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -jwtOptions.Value.ChangeEmailTokenExpiryMinutes
        );
        if (user.ChangeEmailDateTime > expiryTimeAgo)
        {
            throw new NotifyingException($"pleaseCheckYourNewEmailForLinkOrRetryAfter2hours");
        }
        user.NewEmail = changeEmail.NewEmail;
        await userRepository.SaveAsync();

        tokenWriteService.WriteChangeEmailToken(user, out string token);
        user.ChangeEmailDateTime = DateTime.Now;
        await userRepository.SaveAsync();

        _ = Task.Run(() =>
        {
            mailerChangeEmailService.SendChangeEmailEmail(user, token);
        });
        throw new NotifyingException("verificationLinkHasBeenSentToYourNewAddress");
    }

    public void ChangeEmailKey(string ceToken)
    {
        VerifyChangeEmailKey(ceToken, out User? user);
        if (user == null || user.NewEmail.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("somethingGoneWrong");
        }
        user.Email = user.NewEmail!;
        user.ChangeEmailDateTime = null;
        var blockedChangeEmailToken = new BlockedChangeEmailToken
        {
            Token = ceToken,
            DateTime = DateTime.Now
        };
        blockedTokenRepository.Create(blockedChangeEmailToken, "BlockedChangeEmailToken");
        blockedTokenRepository.Save();
    }

    private void VerifyChangeEmailKey(string token, out User? user)
    {
        if (
            blockedTokenRepository.Has(bt =>
                bt.Token == token && bt.Discriminator == "BlockedChangeEmailToken"
            )
        )
        {
            throw new BlockedTokenException($"sessionExpired");
        }
        var tokenD = Uri.UnescapeDataString(token);
        if (!tokenValidationService.IsValidChangeEmailToken(tokenD))
        {
            throw new TokenInvalidException($"invalidAuthorizationKey");
        }

        var userId = tokenReadService.GetUserId(tokenD);
        user = userRepository.Get(u=> u.Id.Equals(userId));
        if (user == null)
        {
            throw new TokenDataException("dataInvalidAuthorizationKey");
        }
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -jwtOptions.Value.ChangeEmailTokenExpiryMinutes
        );
        if (user.ChangeEmailDateTime == null || user.ChangeEmailDateTime < expiryTimeAgo)
        {
            throw new TokenExpiredException($"yourChangeEmailHasExpired");
        }
    }
}
