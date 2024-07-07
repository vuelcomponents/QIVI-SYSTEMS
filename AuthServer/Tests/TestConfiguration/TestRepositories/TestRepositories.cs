using authServer.Data;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.LicenceRepository;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;

namespace authServer.Tests.TestConfiguration.TestRepositories;

public class TestRepositories(AuthDataContext testContext)
{
    public readonly IUserRepository UserRepository = new UserRepository(
        testContext
    );
    public readonly ILicenceRepository LicenceRepository =
        new LicenceRepository(testContext);
    public readonly IBlockedTokenRepository BlockedTokenRepository =
        new BlockedTokenRepository(testContext);
    public readonly ISessionTokenRepository SessionTokenRepository =
        new SessionTokenRepository(testContext);
    public readonly INotificationRepository SettableNotificationRepository =
        new NotificationRepository(testContext);
}
