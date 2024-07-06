namespace authServer.Services.UserService.FactoryUserService;

public interface IFactoryUserService
{
    IUserAdminService CreateUserAdminService();
    IUserBlockService CreateUserBlockService();
    IUserCreateService CreateUserCreateService();
    IUserDeleteService CreateUserDeleteService();
    IUserNotifyService CreateUserNotifyService();
    IUserUpdateService CreateUserUpdateService();
}
