using authServer.Options;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using authServer.Services.UserService;
using authServer.Services.UserService.FactoryUserService;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestServices;

public class TestUserService
{
    public readonly IUserAdminService UserAdminService;
    public readonly IUserBlockService UserBlockService;
    public readonly IUserDeleteService UserDeleteService;
    public readonly IUserCreateService UserCreateService;
    public readonly IUserNotifyService UserNotifyService;
    public readonly IUserUpdateService UserUpdateService;

    public TestUserService(
        TestRepositories.TestRepositories testRepositories,
        IMapper mapper,
        IQuickActions quickActions,
        IMailerVerificationService mailerVerificationService,
        ITokenWriteService tokenWriteService,
        IOptions<JwtOptions> jwtOptions
    )
    {
        IFactoryUserService factoryUserService = new FactoryUserService(
            mapper,
            quickActions,
            mailerVerificationService,
            testRepositories.GettableUserRepository,
            testRepositories.SettableUserRepository,
            tokenWriteService,
            jwtOptions
        );

        UserAdminService = factoryUserService.CreateUserAdminService();
        UserBlockService = factoryUserService.CreateUserBlockService();
        UserCreateService = factoryUserService.CreateUserCreateService();
        UserDeleteService = factoryUserService.CreateUserDeleteService();
        UserNotifyService = factoryUserService.CreateUserNotifyService();
        UserUpdateService = factoryUserService.CreateUserUpdateService();
    }
}
