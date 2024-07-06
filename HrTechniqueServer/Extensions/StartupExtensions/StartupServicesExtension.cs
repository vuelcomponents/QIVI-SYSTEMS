using HrTechniqueServer.Services;

namespace HrTechniqueServer.Extensions.StartupExtensions;

public static class StartupServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        return services;
    }

    public static IServiceCollection AddAuthConnector(this IServiceCollection services)
    {
        services.AddScoped<IAuthServiceConnector, AuthServiceConnector>();
        return services;
    }
}
