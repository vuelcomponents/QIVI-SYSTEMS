using ClassLibrary.Utils;

namespace HrTechniqueServer.Extensions.StartupExtensions;

public static class StartupToolsExtension
{
    public static IServiceCollection AddSharedTools(this IServiceCollection services)
    {
        services.AddSingleton(QuickActions.Instance);
        return services;
    }
}
