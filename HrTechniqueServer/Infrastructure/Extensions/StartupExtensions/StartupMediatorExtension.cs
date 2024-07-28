namespace HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;

public static class StartupMediatorExtension
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        return services;
    }
}
