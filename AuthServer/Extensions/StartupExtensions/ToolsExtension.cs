using ClassLibrary.Utils;

namespace authServer.Extensions.StartupExtensions;

public static class ToolsExtension
{
    public static IServiceCollection AddSharedTools(this IServiceCollection services)
    {
        services.AddSingleton<IQuickActions>(QuickActions.Instance);
        return services;
    }
}
