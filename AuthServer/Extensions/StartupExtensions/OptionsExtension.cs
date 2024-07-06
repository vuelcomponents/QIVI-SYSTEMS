using authServer.Options;

namespace authServer.Extensions.StartupExtensions;

public static class OptionsExtension
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<ConnectionStrings>(options =>
            configuration.GetSection("ConnectionStrings").Bind(options)
        );
        services.Configure<JwtOptions>(options =>
            configuration.GetSection("JwtOptions").Bind(options)
        );
        services.Configure<EmailOptions>(options =>
            configuration.GetSection("EmailOptions").Bind(options)
        );
        return services;
    }
}
