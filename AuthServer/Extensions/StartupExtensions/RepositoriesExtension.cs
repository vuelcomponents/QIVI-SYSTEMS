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
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILicenceRepository, LicenceRepository>();
        services.AddScoped<IBlockedTokenRepository, BlockedTokenRepository>();
        services.AddScoped<ISessionTokenRepository, SessionTokenRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;
    }
}
