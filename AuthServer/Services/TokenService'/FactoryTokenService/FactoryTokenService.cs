using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using Microsoft.Extensions.Options;

namespace authServer.Services.TokenService_.FactoryTokenService;

public class FactoryTokenService(
    ISessionTokenRepository sessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IBlockedTokenRepository blockedTokenRepository
)
    : BaseTokenService(
        sessionTokenRepository,
        jwtOptions,
        blockedTokenRepository
    ),
        IFactoryTokenService
{
    public ITokenExpiredRemovalService CreateTokenExpiredRemovalService()
    {
        return new TokenExpiredRemovalService(
            SessionTokenRepository,
            JwtOptions,
            BlockedTokenRepository
        );
    }

    public ITokenReadService CreateTokenReadService()
    {
        return new TokenReadService(
            SessionTokenRepository,
            JwtOptions,
            BlockedTokenRepository
        );
    }

    public ITokenValidationService CreateTokenValidationService()
    {
        return new TokenValidationService(
            SessionTokenRepository,
            JwtOptions,
            BlockedTokenRepository
        );
    }

    public ITokenWriteService CreateTokenWriteService()
    {
        return new TokenWriteService(
            SessionTokenRepository,
            JwtOptions,
            BlockedTokenRepository
        );
    }

    public ITokenBlockService CreateTokenBlockService()
    {
        return new TokenBlockService(
            SessionTokenRepository,
            JwtOptions,
            BlockedTokenRepository
        );
    }
}
