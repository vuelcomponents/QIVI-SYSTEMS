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
    IUserRepository userRepository,
    IBlockedTokenRepository blockedTokenRepository,
    ILicenceRepository licenceRepository,
    ISessionTokenRepository sessionTokenRepository,
    INotificationRepository settableNotificationRepository,
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
        userRepository,
        blockedTokenRepository,
        licenceRepository,
        sessionTokenRepository,
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
            UserRepository,
            SessionTokenRepository,
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
            UserRepository,
            SettableNotificationRepository,
            Mapper,
            QuickActions
        );
    }

    public IChangeEmailService CreateChangeEmailService()
    {
        return new ChangeEmailService(
            UserRepository,
            BlockedTokenRepository,
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
            UserRepository,
            Mapper,
            HashService
        );
    }

    public ILoginService CreateLoginService()
    {
        return new LoginService(
            UserRepository,
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
            LicenceRepository,
            UserRepository,
            Mapper,
            HashService
        );
    }

    public IResetPasswordService CreateResetPasswordService()
    {
        return new ResetPasswordService(
            UserRepository,
            BlockedTokenRepository,
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
            UserRepository,
            BlockedTokenRepository,
            TokenReadService,
            TokenValidationService,
            resetPasswordService,
            loginService
        );
    }
}
