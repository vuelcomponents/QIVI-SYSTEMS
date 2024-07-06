using authServer.Attributes;
using authServer.Data;
using AutoMapper;

namespace authServer.Tests.TestConfiguration;

public abstract class TestConfiguration : IDisposable
{
    /* test dependencies */
    private readonly IServiceProvider _serviceProvider = TestEnvironment.GetServiceProvider();
    private readonly IConfigurationRoot _configuration = TestEnvironment.GetIConfigurationRoot();
    private readonly AuthDataContext _testContext = TestEnvironment
        .GetServiceProvider()
        .GetRequiredService<AuthTestContext>();

    /* services */
    protected readonly TestServices.TestServices TestServices;

    /* entities */
    protected readonly TestEntities.TestEntities TestEntities;

    protected TestConfiguration()
    {
        void EnsureDatabaseCreated() => _testContext.Database.EnsureCreated();
        EnsureDatabaseCreated();

        /* test options => IOptions => appSettings.json */
        var testOptions = new TestOptions.TestOptions(_configuration);

        /* entities init */
        TestEntities = new(_testContext);

        /* AuthDataAccessor -> auth session data */
        var authDataAccessor = new AuthDataAccessor { Device = TestEntities.AdminDevice };

        /* tools */
        var testTools = new TestTools.TestTools();

        /* repositories */
        var testRepositories = new TestRepositories.TestRepositories(_testContext);

        /* AutoMapper */
        var mapper = _serviceProvider.GetRequiredService<IMapper>();

        /* services */
        TestServices = new(
            testRepositories,
            mapper,
            testTools.QuickActions.Object,
            testOptions.jwtOptions,
            testOptions.emailOptions,
            _testContext,
            testTools.HashService,
            authDataAccessor
        );
    }

    public void Dispose()
    {
        _testContext.Database.EnsureDeleted();
    }
}
