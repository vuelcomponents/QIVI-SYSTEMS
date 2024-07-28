using HrTechniqueServer.Infrastructure.Clients;

namespace HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;

public static class StartupServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        // services.AddScoped<IAuthServiceConnector, AuthServiceConnector>();
        services.AddTransient<AuthClient>();
        return services;
    }
}
