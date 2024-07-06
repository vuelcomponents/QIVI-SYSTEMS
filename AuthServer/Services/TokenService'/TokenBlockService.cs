using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Services.TokenService_.FactoryTokenService;
using ClassLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.TokenService_;

public interface ITokenBlockService
{
    Task Block(string? token);
    Task BlockRefresh(string? refreshToken);
    bool IsTokenBlocked(string token);
    bool IsDeviceVerificationTokenBlocked(string token);
    bool IsVerificationTokenBlocked(string token);
    bool IsChangeEmailTokenBlocked(string token);
    bool IsResetPasswordTokenBlocked(string token);
    bool IsTokenRefreshBlocked(string refreshToken);
}

public class TokenBlockService(
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
        ITokenBlockService
{
    public async Task Block(string? token)
    {
        if (token.IsNullOrEmpty())
        {
            throw new Exception("noAuthorizationKeyProvided");
        }
        var blockedToken = new BlockedToken { Token = token!, DateTime = DateTime.Now };
        SettableBlockedTokenRepository.Create(blockedToken, "BlockedToken");

        var st = GettableSessionTokenRepository.Get(s => s.Token.Equals(token));
        if (st != null)
        {
            SettableSessionTokenRepository.DeleteMany([st]);
        }
        await SettableSessionTokenRepository.SaveAsync();
    }

    public async Task BlockRefresh(string? refreshToken)
    {
        if (refreshToken.IsNullOrEmpty())
        {
            throw new Exception("noAuthorizationKeyProvided");
        }

        var blockedRefreshToken = new BlockedRefreshToken
        {
            Token = refreshToken!,
            DateTime = DateTime.Now
        };
        SettableBlockedTokenRepository.Create(blockedRefreshToken, "BlockedRefreshToken");
        await SettableBlockedTokenRepository.SaveAsync();
    }

    public bool IsTokenBlocked(string token)
    {
        var dbTk = GettableBlockedTokenRepository.GetByToken(token);
        return dbTk != null;
    }

    public bool IsDeviceVerificationTokenBlocked(string token)
    {
        var dbTk = GettableBlockedTokenRepository.GetByToken(token);
        return dbTk != null;
    }

    public bool IsVerificationTokenBlocked(string token)
    {
        var dbTk = GettableBlockedTokenRepository.GetByToken(token);
        return dbTk != null;
    }

    public bool IsChangeEmailTokenBlocked(string token)
    {
        var dbTk = GettableBlockedTokenRepository.GetByToken(token);
        return dbTk != null;
    }

    public bool IsResetPasswordTokenBlocked(string token)
    {
        var dbTk = GettableBlockedTokenRepository.GetByToken(token);
        return dbTk != null;
    }

    public bool IsTokenRefreshBlocked(string refreshToken)
    {
        var dbTk = GettableBlockedTokenRepository.GetByToken(refreshToken);
        return dbTk != null;
    }
}
