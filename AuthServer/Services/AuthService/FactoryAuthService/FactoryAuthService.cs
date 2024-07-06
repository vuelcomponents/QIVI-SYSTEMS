using authServer.Attributes;
using authServer.Options;
using authServer.Repositories.BlockedTokenRepository;
using authServer.Repositories.LicenceRepository;
using authServer.Repositories.NotificationRepository;
using authServer.Repositories.SessionTokenRepository;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.AuthService.FactoryAuthService;

public class FactoryAuthService(
    IGettableUserRepository gettableUserRepository,
    ISettableUserRepository settableUserRepository,
    ISettableBlockedTokenRepository settableBlockedTokenRepository,
    IGettableBlockedTokenRepository gettableBlockedTokenRepository,
    IGettableLicenceRepository gettableLicenceRepository,
    ISettableSessionTokenRepository settableSessionTokenRepository,
    ISettableNotificationRepository settableNotificationRepository,
    IMapper mapper,
    ITokenBlockService tokenBlockService,
    ITokenExpiredRemovalService tokenExpiredRemovalService,
    ITokenReadService tokenReadService,
    ITokenValidationService tokenValidationService,
    ITokenWriteService tokenWriteService,
    IQuickActions quickActions,
    IMailerChangeEmailService mailerChangeEmailService,
    IMailerResetPasswordService mailerResetPasswordService,
    IMailerVerificationService mailerVerificationService,
    IHashService hashService,
    IOptions<JwtOptions> jwtOptions,
    IAuthDataAccessor authDataAccessor
)
    : BaseAuthService(
        gettableUserRepository,
        settableUserRepository,
        settableBlockedTokenRepository,
        gettableBlockedTokenRepository,
        gettableLicenceRepository,
        settableSessionTokenRepository,
        settableNotificationRepository,
        mapper,
        tokenBlockService,
        tokenExpiredRemovalService,
        tokenReadService,
        tokenValidationService,
        tokenWriteService,
        quickActions,
        mailerChangeEmailService,
        mailerResetPasswordService,
        mailerVerificationService,
        hashService,
        jwtOptions,
        authDataAccessor
    ),
        IFactoryAuthService
{
    public IAuthorizationService CreateAuthorizationService()
    {
        return new AuthorizationService(
            GettableUserRepository,
            SettableSessionTokenRepository,
            Mapper,
            TokenBlockService,
            TokenReadService,
            TokenValidationService,
            TokenWriteService,
            JwtOptions,
            AuthDataAccessor
        );
    }

    public IAuthUserService CreateAuthUserService()
    {
        return new AuthUserService(
            GettableUserRepository,
            SettableUserRepository,
            SettableNotificationRepository,
            Mapper,
            QuickActions
        );
    }

    public IChangeEmailService CreateChangeEmailService()
    {
        return new ChangeEmailService(
            GettableUserRepository,
            SettableBlockedTokenRepository,
            GettableBlockedTokenRepository,
            TokenWriteService,
            TokenReadService,
            TokenValidationService,
            MailerChangeEmailService,
            HashService,
            JwtOptions
        );
    }

    public IChangePasswordService CreateChangePasswordService()
    {
        return new ChangePasswordService(
            GettableUserRepository,
            SettableUserRepository,
            Mapper,
            HashService
        );
    }

    public ILoginService CreateLoginService()
    {
        return new LoginService(
            GettableUserRepository,
            SettableUserRepository,
            TokenWriteService,
            MailerVerificationService,
            HashService,
            JwtOptions,
            Mapper
        );
    }

    public ILogoutService CreateLogoutService()
    {
        return new LogoutService(TokenBlockService);
    }

    public IRegisterSuperAdminService CreateRegisterSuperAdminService()
    {
        return new RegisterSuperAdminService(
            GettableLicenceRepository,
            SettableUserRepository,
            Mapper,
            HashService
        );
    }

    public IResetPasswordService CreateResetPasswordService()
    {
        return new ResetPasswordService(
            GettableUserRepository,
            SettableBlockedTokenRepository,
            GettableBlockedTokenRepository,
            TokenWriteService,
            TokenReadService,
            TokenValidationService,
            MailerResetPasswordService,
            HashService,
            JwtOptions
        );
    }

    public IVerificationService CreateVerificationService()
    {
        IResetPasswordService resetPasswordService = CreateResetPasswordService();
        ILoginService loginService = CreateLoginService();
        return new VerificationService(
            GettableUserRepository,
            SettableBlockedTokenRepository,
            GettableBlockedTokenRepository,
            TokenReadService,
            TokenValidationService,
            resetPasswordService,
            loginService
        );
    }
}
