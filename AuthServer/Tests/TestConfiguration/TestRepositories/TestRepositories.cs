using authServer.Data;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.LicenceRepository;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;

namespace authServer.Tests.TestConfiguration.TestRepositories;

public class TestRepositories(AuthDataContext testContext)
{
    public readonly IGettableUserRepository GettableUserRepository = new GettableUserRepository(
        testContext
    );
    public readonly ISettableUserRepository SettableUserRepository = new SettableUserRepository(
        testContext
    );
    public readonly IGettableLicenceRepository GettableLicenceRepository =
        new GettableLicenceRepository(testContext);
    public readonly ISettableLicenceRepository SettableLicenceRepository =
        new SettableLicenceRepository(testContext);
    public readonly IGettableBlockedTokenRepository GettableBlockedTokenRepository =
        new GettableBlockedTokenRepository(testContext);
    public readonly ISettableBlockedTokenRepository SettableBlockedTokenRepository =
        new SettableBlockedTokenRepository(testContext);
    public readonly IGettableSessionTokenRepository GettableSessionTokenRepository =
        new GettableSessionTokenRepository(testContext);
    public readonly ISettableSessionTokenRepository SettableSessionTokenRepository =
        new SettableSessionTokenRepository(testContext);
    public readonly ISettableNotificationRepository SettableNotificationRepository =
        new SettableNotificationRepository(testContext);
}
