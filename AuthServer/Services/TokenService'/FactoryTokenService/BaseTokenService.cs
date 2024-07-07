using System.Text;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.TokenService_.FactoryTokenService;

public abstract class BaseTokenService(
    ISessionTokenRepository sessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IBlockedTokenRepository blockedTokenRepository
)
{
    protected ISessionTokenRepository SessionTokenRepository =
        sessionTokenRepository;
    protected IOptions<JwtOptions> JwtOptions = jwtOptions;
    protected IBlockedTokenRepository BlockedTokenRepository =
        blockedTokenRepository;

    protected static TokenValidationParameters GetValidationParameters(string secret)
    {
        return new TokenValidationParameters
        {
            ClockSkew = TimeSpan.FromMinutes(0),
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };
    }
}
