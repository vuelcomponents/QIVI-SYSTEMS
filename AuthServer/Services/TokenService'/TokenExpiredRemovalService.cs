using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Services.TokenService_.FactoryTokenService;
using Microsoft.Extensions.Options;

namespace authServer.Services.TokenService_;

public interface ITokenExpiredRemovalService
{
    Task RemoveExpiredTokens();
    Task RemoveExpiredDeviceVerifyTokens();
    Task RemoveExpiredSessionTokens();
    Task RemoveExpiredVerifyTokens();
    Task RemoveExpiredResetPasswordTokens();
    Task RemoveExpiredRefreshTokens();
    Task RemoveExpiredChangeEmailTokens();
}

public class TokenExpiredRemovalService(
    ISessionTokenRepository sessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IBlockedTokenRepository blockedTokenRepository
)
    : BaseTokenService(
        sessionTokenRepository,
        jwtOptions,
        blockedTokenRepository
    ),
        ITokenExpiredRemovalService
{
    public async Task RemoveExpiredTokens()
    {
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(-JwtOptions.Value.TokenExpiryMinutes);
        try
        {
            var expiredTokens = BlockedTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;

            BlockedTokenRepository.DeleteMany(expiredTokens);
            await BlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task RemoveExpiredDeviceVerifyTokens()
    {
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -JwtOptions.Value.DeviceVerificationTokenExpiryMinutes
        );
        try
        {
            var expiredTokens = BlockedTokenRepository.GetAll(t =>
                t.Discriminator == "BlockedDeviceVerifyToken" && t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;
            BlockedTokenRepository.DeleteMany(expiredTokens);
            await BlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task RemoveExpiredSessionTokens()
    {
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(-JwtOptions.Value.TokenExpiryMinutes);
        try
        {
            var expiredTokens = await SessionTokenRepository.GetAllAsync(t =>
                t.DateTime < expiryTimeAgo
            );

            SessionTokenRepository.DeleteMany(expiredTokens);

            if (expiredTokens.Count < 1)
                return;
            try
            {
                await SessionTokenRepository.SaveAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.InnerException!.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task RemoveExpiredVerifyTokens()
    {
        DateTime expiryAgo = DateTime.Now.AddMinutes(
            -JwtOptions.Value.VerificationTokenExpiryMinutes
        );
        try
        {
            var expiredTokens = BlockedTokenRepository.GetAll(t => t.DateTime < expiryAgo);

            if (expiredTokens.Count < 1)
                return;

            BlockedTokenRepository.DeleteMany(expiredTokens);
            await BlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task RemoveExpiredResetPasswordTokens()
    {
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -JwtOptions.Value.ResetPasswordTokenExpiryMinutes + 4
        );
        try
        {
            var expiredTokens = BlockedTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;

            BlockedTokenRepository.DeleteMany(expiredTokens);
            await BlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task RemoveExpiredRefreshTokens()
    {
        DateTime threeHoursAgo = DateTime.Now.AddMinutes(
            -JwtOptions.Value.RefreshTokenExpiryMinutes
        );
        try
        {
            var expiredTokens = BlockedTokenRepository.GetAll(t =>
                t.DateTime < threeHoursAgo
            );

            if (expiredTokens.Count < 1)
                return;
            BlockedTokenRepository.DeleteMany(expiredTokens);
            await BlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task RemoveExpiredChangeEmailTokens()
    {
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(
            -JwtOptions.Value.ChangeEmailTokenExpiryMinutes
        );
        try
        {
            var expiredTokens = BlockedTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;

            BlockedTokenRepository.DeleteMany(expiredTokens);
            await BlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
