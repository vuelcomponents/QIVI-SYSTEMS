using authServer.Attributes;
using authServer.Options;
using authServer.Services;
using authServer.Services.AuthService;
using authServer.Services.AuthService.FactoryAuthService;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestServices;

public class TestAuthService
{
    public readonly IAuthorizationService AuthorizationService;
    public readonly IAuthUserService AuthUserService;
    public readonly IChangeEmailService ChangeEmailService;
    public readonly IChangePasswordService ChangePasswordService;
    public readonly ILoginService LoginService;
    public readonly ILogoutService LogoutService;
    public readonly IRegisterSuperAdminService RegisterSuperAdminService;
    public readonly IResetPasswordService ResetPasswordService;
    public readonly IVerificationService VerificationService;

    public TestAuthService(
        TestRepositories.TestRepositories testRepositories,
        TestTokenService testTokenService,
        TestMailerService testMailerService,
        IMapper mapper,
        IOptions<JwtOptions> jwtOptions,
        IHashService hashService,
        IQuickActions quickActions,
        IAuthDataAccessor authDataAccessor
    )
    {
        IFactoryAuthService factoryAuthService = new FactoryAuthService(
            testRepositories.UserRepository,
            testRepositories.BlockedTokenRepository,
            testRepositories.LicenceRepository,
            testRepositories.SessionTokenRepository,
            testRepositories.SettableNotificationRepository,
            mapper,
            testTokenService.TokenBlockService,
            testTokenService.TokenExpiredRemovalService,
            testTokenService.TokenReadService,
            testTokenService.TokenValidationService,
            testTokenService.TokenWriteService,
            quickActions,
            testMailerService.MailerChangeEmailService,
            testMailerService.MailerResetPasswordService,
            testMailerService.MailerVerificationService,
            hashService,
            jwtOptions,
            authDataAccessor
        );
        AuthorizationService = factoryAuthService.CreateAuthorizationService();
        AuthUserService = factoryAuthService.CreateAuthUserService();
        ChangeEmailService = factoryAuthService.CreateChangeEmailService();
        ChangePasswordService = factoryAuthService.CreateChangePasswordService();
        LoginService = factoryAuthService.CreateLoginService();
        LogoutService = factoryAuthService.CreateLogoutService();
        RegisterSuperAdminService = factoryAuthService.CreateRegisterSuperAdminService();
        ResetPasswordService = factoryAuthService.CreateResetPasswordService();
        VerificationService = factoryAuthService.CreateVerificationService();
    }
}
