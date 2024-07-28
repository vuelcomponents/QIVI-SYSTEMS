using ClassLibrary.Utils;

namespace HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;

public static class StartupToolsExtension
{
    public static IServiceCollection AddSharedTools(this IServiceCollection services)
    {
        services.AddSingleton(QuickActions.Instance);
        return services;
    }
}
