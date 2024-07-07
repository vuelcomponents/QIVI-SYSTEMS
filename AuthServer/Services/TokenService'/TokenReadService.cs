using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Services.TokenService_.FactoryTokenService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.TokenService_;

public interface ITokenReadService
{
    Int64 GetUserId(string token);
    TimeSpan GetTimeToExpiration(string token);
}

public class TokenReadService(
    ISessionTokenRepository sessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IBlockedTokenRepository blockedTokenRepository
)
    : BaseTokenService(
        sessionTokenRepository,
        jwtOptions,
        blockedTokenRepository
    ),
        ITokenReadService
{
    public Int64 GetUserId(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        if (securityToken == null)
        {
            throw new Exception("dataInvalidAuthorizationKey");
        }
        var userIdClaim = securityToken.Claims.FirstOrDefault(x =>
            x.Type == ClaimTypes.NameIdentifier
        );
        if (userIdClaim != null && Int64.TryParse(userIdClaim.Value, out long userId))
        {
            return userId;
        }

        throw new Exception("somethingGoneWrong");
    }

    public TimeSpan GetTimeToExpiration(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(
            token,
            GetValidationParameters(JwtOptions.Value.SecretTokenKey),
            out SecurityToken validatedToken
        );
        var jwtToken = (JwtSecurityToken)validatedToken;

        var expirationTime = jwtToken.ValidTo;
        var timeToExpiration = expirationTime - DateTime.UtcNow;

        return timeToExpiration;
    }
}
