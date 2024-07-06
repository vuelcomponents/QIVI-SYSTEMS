using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Extensions.StartupExtensions;

public static class AuthExtension
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        string secretTokenKey
    )
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(0),
                    IgnoreTrailingSlashWhenValidatingAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretTokenKey)
                    ),
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    RequireAudience = false,
                    RequireSignedTokens = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true
                    // ValidAudience = tokenOptions.Audience,
                    // ValidIssuer = tokenOptions.Issuer
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = $"{context.Request.Cookies["hrtechniquetoken"]}";
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }
}
