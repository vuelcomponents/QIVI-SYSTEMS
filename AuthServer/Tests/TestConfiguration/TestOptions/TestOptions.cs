using authServer.Options;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestOptions;

public class TestOptions(IConfigurationRoot configurationRoot)
{
    public readonly IOptions<JwtOptions> jwtOptions = Microsoft.Extensions.Options.Options.Create(
        configurationRoot.GetSection("JwtOptions").Get<JwtOptions>()!
    );
    public readonly IOptions<EmailOptions> emailOptions =
        Microsoft.Extensions.Options.Options.Create(
            configurationRoot.GetSection("EmailOptions").Get<EmailOptions>()!
        );
}
