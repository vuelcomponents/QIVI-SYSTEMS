using authServer.Attributes;

namespace authServer.Extensions.StartupExtensions;

public static class ContextExtension
{
    public static IServiceCollection AddContextData(this IServiceCollection services)
    {
        services.AddScoped<IAuthDataAccessor, AuthDataAccessor>();
        return services;
    }
}
