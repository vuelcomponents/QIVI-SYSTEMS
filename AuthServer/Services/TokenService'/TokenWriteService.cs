using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Services.TokenService_.FactoryTokenService;
using ClassLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Services.TokenService_;

public interface ITokenWriteService
{
    void WriteToken(User dbUser, out string token, out string refreshToken);
    void WriteVerificationToken(User dbUser, out string token);
    void WriteResetPasswordToken(User dbUser, out string token);
    void WriteChangeEmailToken(User dbUser, out string token);
    void WriteDeviceVerificationToken(User dbUser, out string token);
    void WriteExpiredTestToken(User dbUser, out string token, out string refreshToken);
}

public class TokenWriteService(
    ISessionTokenRepository sessionTokenRepository,
    IOptions<JwtOptions> jwtOptions,
    IBlockedTokenRepository blockedTokenRepository
)
    : BaseTokenService(
        sessionTokenRepository,
        jwtOptions,
        blockedTokenRepository
    ),
        ITokenWriteService
{
    public void WriteToken(User dbUser, out string token, out string refreshToken)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset.Now.AddMinutes(JwtOptions.Value.TokenExpiryMinutes).DateTime,
            signingCredentials: credentials
        );

        token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        refreshToken = WriteRefreshToken();
    }

    public void WriteVerificationToken(User dbUser, out string token)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretVerificationTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset
                .Now.AddMinutes(JwtOptions.Value.VerificationTokenExpiryMinutes)
                .DateTime,
            signingCredentials: credentials
        );

        token = new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public void WriteResetPasswordToken(User dbUser, out string token)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretResetPasswordTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset
                .Now.AddMinutes(JwtOptions.Value.ResetPasswordTokenExpiryMinutes)
                .DateTime,
            signingCredentials: credentials
        );

        token = new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public void WriteChangeEmailToken(User dbUser, out string token)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretChangeEmailTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset
                .Now.AddMinutes(JwtOptions.Value.ChangeEmailTokenExpiryMinutes)
                .DateTime,
            signingCredentials: credentials
        );
        token = new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private string WriteRefreshToken()
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretRefreshTokenTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // var claims = new List<Claim>();

        var refreshToken = new JwtSecurityToken(
            // claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset
                .Now.AddMinutes(JwtOptions.Value.RefreshTokenExpiryMinutes)
                .DateTime,
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
    }

    public void WriteDeviceVerificationToken(User dbUser, out string token)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretDeviceVerifyTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset
                .Now.AddMinutes(JwtOptions.Value.DeviceVerificationTokenExpiryMinutes)
                .DateTime,
            signingCredentials: credentials
        );
        token = new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public void WriteExpiredTestToken(User dbUser, out string token, out string refreshToken)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtOptions.Value.SecretTokenKey)
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTimeOffset.Now.DateTime,
            signingCredentials: credentials
        );

        token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        refreshToken = WriteRefreshToken();
    }
}
