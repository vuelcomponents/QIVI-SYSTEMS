using authServer.Exceptions;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.UserRepository;
using authServer.Services.TokenService_;
using ClassLibrary.Models;

namespace authServer.Services.AuthService;

public interface IVerificationService
{
    Task<string> VerifyEmail(string verificationToken);
    Task TrySendVerificationToUser(string email);
}

public class VerificationService(
    IGettableUserRepository gettableUserRepository,
    ISettableBlockedTokenRepository settableBlockedTokenRepository,
    IGettableBlockedTokenRepository gettableBlockedTokenRepository,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    IResetPasswordService resetPasswordService,
    ILoginService loginService
) : IVerificationService
{
    public async Task<string> VerifyEmail(string verificationToken)
    {
        if (!tokenValidationService.IsValidVerifyToken(verificationToken))
        {
            throw new TokenInvalidException("invalidAuthorizationKey");
        }

        if (
            gettableBlockedTokenRepository.Has(bt =>
                bt.Token == verificationToken && bt.Discriminator == "BlockedVerifyToken"
            )
        )
        {
            throw new BlockedTokenException("sessionExpired");
        }

        Int64 userId = tokenReadService.GetUserId(verificationToken);
        User? dbUser = await gettableUserRepository.GetByIdAsync(userId);
        if (dbUser == null)
        {
            throw new TokenDataException("dataInvalidAuthorizationKey");
        }

        if (dbUser.Verified is true)
        {
            throw new AlreadyVerifiedException("youHaveAlreadyVerifiedYourAccount");
        }

        dbUser.Verified = true;
        dbUser.VerifyEmailDateTime = null;

        var blockedVerifyToken = new BlockedVerifyToken
        {
            Token = verificationToken,
            DateTime = DateTime.Now
        };
        settableBlockedTokenRepository.Create(blockedVerifyToken, "BlockedVerifyToken");
        await settableBlockedTokenRepository.SaveAsync();

        resetPasswordService.InitResetPassword(dbUser.Email, true, out string token, false);
        return token;
    }

    public async Task TrySendVerificationToUser(string email)
    {
        var dbUser =
            await gettableUserRepository.GetByEmailAsync(email)
            ?? throw new UserDoesNotExistException("userDoesNotExist");
        await loginService.HandleUnverifiedUser(dbUser);
        throw new AlreadyVerifiedException("youHaveAlreadyVerifiedYourAccount");
    }
}
