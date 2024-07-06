using System.Text;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.TokenService_.FactoryTokenService;

public abstract class BaseTokenService(
    IGettableSessionTokenRepository gettableSessionTokenRepository,
    ISettableSessionTokenRepository settableSessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IGettableBlockedTokenRepository gettableBlockedTokenRepository,
    ISettableBlockedTokenRepository settableBlockedTokenRepository
)
{
    protected IGettableSessionTokenRepository GettableSessionTokenRepository =
        gettableSessionTokenRepository;
    protected ISettableSessionTokenRepository SettableSessionTokenRepository =
        settableSessionTokenRepository;
    protected IOptions<JwtOptions> JwtOptions = jwtOptions;
    protected IGettableBlockedTokenRepository GettableBlockedTokenRepository =
        gettableBlockedTokenRepository;
    protected ISettableBlockedTokenRepository SettableBlockedTokenRepository =
        settableBlockedTokenRepository;

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
