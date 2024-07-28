using HrTechniqueServer.Shared.Resources;

namespace HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;

public static class StartupResourcesExtension
{
    public static IServiceCollection AddResources(
        this IServiceCollection services
    )
    {
        services.AddScoped<IAuthResource, AuthResource>();
        return services;
    }
}