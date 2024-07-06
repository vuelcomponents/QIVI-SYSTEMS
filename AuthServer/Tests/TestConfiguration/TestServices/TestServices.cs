using authServer.Attributes;
using authServer.Data;
using authServer.Options;
using authServer.Services;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestServices;

public class TestServices
{
    public readonly TestUserService TestUserService;
    public readonly TestDeviceService TestDeviceService;
    public readonly TestAuthService TestAuthService;
    public readonly TestMailerService TestMailerService;
    public readonly TestTokenService TestTokenService;

    public TestServices(
        TestRepositories.TestRepositories testRepositories,
        IMapper mapper,
        IQuickActions quickActions,
        IOptions<JwtOptions> jwtOptions,
        IOptions<EmailOptions> emailOptions,
        AuthDataContext testContext,
        IHashService hashService,
        IAuthDataAccessor authDataAccessor
    )
    {
        TestMailerService = new(emailOptions);

        TestTokenService = new(testRepositories, jwtOptions);

        TestUserService = new TestUserService(
            testRepositories,
            mapper,
            quickActions,
            TestMailerService.MailerVerificationService,
            TestTokenService.TokenWriteService,
            jwtOptions
        );

        TestAuthService = new(
            testRepositories,
            TestTokenService,
            TestMailerService,
            mapper,
            jwtOptions,
            hashService,
            quickActions,
            authDataAccessor
        );

        TestDeviceService = new(
            TestTokenService,
            testContext,
            mapper,
            TestMailerService,
            jwtOptions
        );
    }
}
