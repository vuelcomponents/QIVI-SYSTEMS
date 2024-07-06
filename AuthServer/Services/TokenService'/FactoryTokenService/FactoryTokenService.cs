using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using Microsoft.Extensions.Options;

namespace authServer.Services.TokenService_.FactoryTokenService;

public class FactoryTokenService(
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
        IFactoryTokenService
{
    public ITokenExpiredRemovalService CreateTokenExpiredRemovalService()
    {
        return new TokenExpiredRemovalService(
            GettableSessionTokenRepository,
            SettableSessionTokenRepository,
            JwtOptions,
            GettableBlockedTokenRepository,
            SettableBlockedTokenRepository
        );
    }

    public ITokenReadService CreateTokenReadService()
    {
        return new TokenReadService(
            GettableSessionTokenRepository,
            SettableSessionTokenRepository,
            JwtOptions,
            GettableBlockedTokenRepository,
            SettableBlockedTokenRepository
        );
    }

    public ITokenValidationService CreateTokenValidationService()
    {
        return new TokenValidationService(
            GettableSessionTokenRepository,
            SettableSessionTokenRepository,
            JwtOptions,
            GettableBlockedTokenRepository,
            SettableBlockedTokenRepository
        );
    }

    public ITokenWriteService CreateTokenWriteService()
    {
        return new TokenWriteService(
            GettableSessionTokenRepository,
            SettableSessionTokenRepository,
            JwtOptions,
            GettableBlockedTokenRepository,
            SettableBlockedTokenRepository
        );
    }

    public ITokenBlockService CreateTokenBlockService()
    {
        return new TokenBlockService(
            GettableSessionTokenRepository,
            SettableSessionTokenRepository,
            JwtOptions,
            GettableBlockedTokenRepository,
            SettableBlockedTokenRepository
        );
    }
}
