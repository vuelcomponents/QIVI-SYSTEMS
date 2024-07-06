using System.IdentityModel.Tokens.Jwt;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Services.TokenService_.FactoryTokenService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.TokenService_;

public interface ITokenValidationService
{
    bool IsValidToken(string token);
    bool IsValidVerifyToken(string token);
    bool IsValidRefreshToken(string token);
    bool IsValidResetPasswordToken(string token);
    bool IsValidDeviceVerificationToken(string token);
    bool IsValidChangeEmailToken(string token);
}

public class TokenValidationService(
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
        ITokenValidationService
{
    public bool IsValidToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                GetValidationParameters(JwtOptions.Value.SecretTokenKey),
                out _
            );
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsValidVerifyToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                GetValidationParameters(JwtOptions.Value.SecretVerificationTokenKey),
                out _
            );
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsValidRefreshToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                GetValidationParameters(JwtOptions.Value.SecretRefreshTokenTokenKey),
                out _
            );
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsValidResetPasswordToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                GetValidationParameters(JwtOptions.Value.SecretResetPasswordTokenKey),
                out _
            );
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsValidDeviceVerificationToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                GetValidationParameters(JwtOptions.Value.SecretDeviceVerifyTokenKey),
                out _
            );
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsValidChangeEmailToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                GetValidationParameters(JwtOptions.Value.SecretChangeEmailTokenKey),
                out _
            );
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
