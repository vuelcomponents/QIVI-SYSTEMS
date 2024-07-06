using authServer.Services;
using authServer.Services.AuthService;
using authServer.Services.DeviceService;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using authServer.Services.UserService;

namespace authServer.Extensions.StartupExtensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IAuthStatisticService, AuthStatisticService>();

        //user
        services.AddScoped<IUserAdminService, UserAdminService>();
        services.AddScoped<IUserBlockService, UserBlockService>();
        services.AddScoped<IUserCreateService, UserCreateService>();
        services.AddScoped<IUserDeleteService, UserDeleteService>();
        services.AddScoped<IUserNotifyService, UserNotifyService>();
        services.AddScoped<IUserUpdateService, UserUpdateService>();

        //auth
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IAuthUserService, AuthUserService>();
        services.AddScoped<IChangeEmailService, ChangeEmailService>();
        services.AddScoped<IChangePasswordService, ChangePasswordService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ILogoutService, LogoutService>();
        services.AddScoped<IRegisterSuperAdminService, RegisterSuperAdminService>();
        services.AddScoped<IResetPasswordService, ResetPasswordService>();
        services.AddScoped<IVerificationService, VerificationService>();

        //device
        services.AddScoped<IDeviceBlockService, DeviceBlockService>();
        services.AddScoped<IDeviceRetrievalService, DeviceRetrievalService>();
        services.AddScoped<IDeviceSignoutService, DeviceSignoutService>();
        services.AddScoped<IDeviceUnlockService, DeviceUnlockService>();
        services.AddScoped<IDeviceVerificationService, DeviceVerificationService>();
        services.AddScoped<IIpBlockService, IpBlockService>();
        services.AddScoped<IIpUnlockService, IpUnlockService>();

        //mailers
        services.AddScoped<IMailerChangeEmailService, MailerChangeEmailService>();
        services.AddScoped<IMailerResetPasswordService, MailerResetPasswordService>();
        services.AddScoped<IMailerVerificationService, MailerVerificationService>();

        //tokens
        services.AddScoped<ITokenBlockService, TokenBlockService>();
        services.AddScoped<ITokenExpiredRemovalService, TokenExpiredRemovalService>();
        services.AddScoped<ITokenReadService, TokenReadService>();
        services.AddScoped<ITokenValidationService, TokenValidationService>();
        services.AddScoped<ITokenWriteService, TokenWriteService>();

        return services;
    }
}
