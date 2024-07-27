using HrTechniqueServer.Services;

namespace HrTechniqueServer.Extensions.StartupExtensions;

public static class StartupServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddAuthConnector(this IServiceCollection services)
    {
        services.AddScoped<IAuthServiceConnector, AuthServiceConnector>();
        return services;
    }
}
