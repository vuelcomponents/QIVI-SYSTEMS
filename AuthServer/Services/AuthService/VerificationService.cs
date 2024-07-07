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
    IUserRepository userRepository,
    IBlockedTokenRepository blockedTokenRepository,
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
            blockedTokenRepository.Has(bt =>
                bt.Token == verificationToken && bt.Discriminator == "BlockedVerifyToken"
            )
        )
        {
            throw new BlockedTokenException("sessionExpired");
        }

        Int64 userId = tokenReadService.GetUserId(verificationToken);
        User? dbUser = await userRepository.GetAsync(u=> u.Id.Equals(userId));
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
        blockedTokenRepository.Create(blockedVerifyToken, "BlockedVerifyToken");
        await blockedTokenRepository.SaveAsync();

        resetPasswordService.InitResetPassword(dbUser.Email, true, out string token, false);
        return token;
    }

    public async Task TrySendVerificationToUser(string email)
    {
        var dbUser =
            await userRepository.GetAsync(u=>u.Email.Equals(email))
            ?? throw new UserDoesNotExistException("userDoesNotExist");
        await loginService.HandleUnverifiedUser(dbUser);
        throw new AlreadyVerifiedException("youHaveAlreadyVerifiedYourAccount");
    }
}
