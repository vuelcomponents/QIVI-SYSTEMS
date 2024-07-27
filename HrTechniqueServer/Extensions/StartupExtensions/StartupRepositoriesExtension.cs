using HrTechniqueServer.Infrastructure.Persistence.Repositories;

namespace HrTechniqueServer.Extensions.StartupExtensions;

public static class StartupRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<EmployeeRepository, EmployeeRepository>();
        return services;
    }
}
