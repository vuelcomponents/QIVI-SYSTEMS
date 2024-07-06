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

namespace authServer.Services.AuthService;

public interface ILogoutService
{
    Task Logout(IRequestCookieCollection cookies);
}

public class LogoutService(ITokenBlockService tokenBlockService) : ILogoutService
{
    public async Task Logout(IRequestCookieCollection cookies)
    {
        string? token = cookies["hrtechniquetoken"];
        string? refreshToken = cookies["hrtechniquetoken"];

        if (token != null)
        {
            await tokenBlockService.Block(token);
        }
        if (refreshToken != null)
            await tokenBlockService.BlockRefresh(refreshToken);
    }
}
