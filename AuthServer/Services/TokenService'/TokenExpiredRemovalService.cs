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
    IGettableSessionTokenRepository gettableSessionTokenRepository,
    ISettableSessionTokenRepository settableSessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IGettableBlockedTokenRepository gettableBlockedTokenRepository,
    ISettableBlockedTokenRepository settableBlockedTokenRepository
)
    : BaseTokenService(
        gettableSessionTokenRepository,
        settableSessionTokenRepository,
        jwtOptions,
        gettableBlockedTokenRepository,
        settableBlockedTokenRepository
    ),
        ITokenExpiredRemovalService
{
    public async Task RemoveExpiredTokens()
    {
        DateTime expiryTimeAgo = DateTime.Now.AddMinutes(-JwtOptions.Value.TokenExpiryMinutes);
        try
        {
            var expiredTokens = GettableBlockedTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;

            SettableBlockedTokenRepository.DeleteMany(expiredTokens);
            await SettableBlockedTokenRepository.SaveAsync();
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
            var expiredTokens = GettableBlockedTokenRepository.GetAll(t =>
                t.Discriminator == "BlockedDeviceVerifyToken" && t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;
            SettableBlockedTokenRepository.DeleteMany(expiredTokens);
            await SettableBlockedTokenRepository.SaveAsync();
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
            var expiredTokens = GettableSessionTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            SettableSessionTokenRepository.DeleteMany(expiredTokens);

            if (expiredTokens.Count < 1)
                return;
            try
            {
                await SettableSessionTokenRepository.SaveAsync();
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
            var expiredTokens = GettableBlockedTokenRepository.GetAll(t => t.DateTime < expiryAgo);

            if (expiredTokens.Count < 1)
                return;

            SettableBlockedTokenRepository.DeleteMany(expiredTokens);
            await SettableBlockedTokenRepository.SaveAsync();
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
            var expiredTokens = GettableBlockedTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;

            SettableBlockedTokenRepository.DeleteMany(expiredTokens);
            await SettableBlockedTokenRepository.SaveAsync();
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
            var expiredTokens = GettableBlockedTokenRepository.GetAll(t =>
                t.DateTime < threeHoursAgo
            );

            if (expiredTokens.Count < 1)
                return;
            SettableBlockedTokenRepository.DeleteMany(expiredTokens);
            await SettableBlockedTokenRepository.SaveAsync();
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
            var expiredTokens = GettableBlockedTokenRepository.GetAll(t =>
                t.DateTime < expiryTimeAgo
            );

            if (expiredTokens.Count < 1)
                return;

            SettableBlockedTokenRepository.DeleteMany(expiredTokens);
            await SettableBlockedTokenRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
