using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.LicenceRepository;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;

namespace authServer.Extensions.StartupExtensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGettableUserRepository, GettableUserRepository>();
        services.AddScoped<ISettableUserRepository, SettableUserRepository>();
        services.AddScoped<IGettableLicenceRepository, GettableLicenceRepository>();
        services.AddScoped<ISettableLicenceRepository, SettableLicenceRepository>();
        services.AddScoped<IGettableBlockedTokenRepository, GettableBlockedTokenRepository>();
        services.AddScoped<ISettableBlockedTokenRepository, SettableBlockedTokenRepository>();
        services.AddScoped<ISettableSessionTokenRepository, SettableSessionTokenRepository>();
        services.AddScoped<IGettableSessionTokenRepository, GettableSessionTokenRepository>();
        services.AddScoped<ISettableNotificationRepository, SettableNotificationRepository>();
        return services;
    }
}
