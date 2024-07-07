using authServer.Options;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.UserService.FactoryUserService;

public class FactoryUserService(
    IMapper mapper,
    IQuickActions quickActions,
    IMailerVerificationService mailerVerificationService,
    IUserRepository userRepository,
    ITokenWriteService tokenWriteService,
    IOptions<JwtOptions> jwtOptions
)
    : BaseUserService(
        mapper,
        quickActions,
        mailerVerificationService,
        userRepository,
        tokenWriteService,
        jwtOptions
    ),
        IFactoryUserService
{
    public IUserAdminService CreateUserAdminService()
    {
        return new UserAdminService(
            Mapper,
            QuickActions,
            MailerVerificationService,
            UserRepository,
            TokenWriteService,
            JwtOptions
        );
    }

    public IUserBlockService CreateUserBlockService()
    {
        return new UserBlockService(
            Mapper,
            QuickActions,
            MailerVerificationService,
            UserRepository,
            TokenWriteService,
            JwtOptions
        );
    }

    public IUserCreateService CreateUserCreateService()
    {
        return new UserCreateService(
            Mapper,
            QuickActions,
            MailerVerificationService,
            UserRepository,
            TokenWriteService,
            JwtOptions
        );
    }

    public IUserDeleteService CreateUserDeleteService()
    {
        return new UserDeleteService(
            Mapper,
            QuickActions,
            MailerVerificationService,
            UserRepository,
            TokenWriteService,
            JwtOptions
        );
    }

    public IUserNotifyService CreateUserNotifyService()
    {
        return new UserNotifyService(
            Mapper,
            QuickActions,
            MailerVerificationService,
            UserRepository,
            TokenWriteService,
            JwtOptions
        );
    }

    public IUserUpdateService CreateUserUpdateService()
    {
        return new UserUpdateService(
            Mapper,
            QuickActions,
            MailerVerificationService,
            UserRepository,
            TokenWriteService,
            JwtOptions
        );
    }
}
