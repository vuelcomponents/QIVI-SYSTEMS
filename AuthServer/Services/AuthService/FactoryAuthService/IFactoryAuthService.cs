namespace authServer.Services.AuthService.FactoryAuthService;

public interface IFactoryAuthService
{
    IAuthorizationService CreateAuthorizationService();
    IAuthUserService CreateAuthUserService();
    IChangeEmailService CreateChangeEmailService();
    IChangePasswordService CreateChangePasswordService();
    ILoginService CreateLoginService();
    ILogoutService CreateLogoutService();
    IRegisterSuperAdminService CreateRegisterSuperAdminService();
    IResetPasswordService CreateResetPasswordService();
    IVerificationService CreateVerificationService();
}
