using HrTechniqueServer.Options;

namespace HrTechniqueServer.Extensions.StartupExtensions;

public static class StartupOptionsExtension
{
    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
        return services;
    }
}
