using HrTechniqueServer.Repositories;

namespace HrTechniqueServer.Extensions.StartupExtensions;

public static class StartupRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        return services;
    }
}
