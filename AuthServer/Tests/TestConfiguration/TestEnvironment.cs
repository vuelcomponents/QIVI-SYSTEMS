using authServer.Data;

namespace authServer.Tests.TestConfiguration;

public static class TestEnvironment
{
    public static IConfigurationRoot GetIConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static IServiceProvider GetServiceProvider()
    {
        return new ServiceCollection()
            .AddDbContext<AuthTestContext>()
            .AddAutoMapper(typeof(Program).Assembly)
            .BuildServiceProvider();
    }
}
