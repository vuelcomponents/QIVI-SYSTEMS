using authServer.Repositories.UserRepository;
using authServer.Services.TokenService_;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Attributes;

public sealed class AuthDevice : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string? token = context.HttpContext.Request.Cookies["hrtechniquetoken"];
        if (token.IsNullOrEmpty())
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        var tokenReadService =
            context.HttpContext.RequestServices.GetRequiredService<ITokenReadService>();

        var dataAccessor =
            context.HttpContext.RequestServices.GetRequiredService<IAuthDataAccessor>();
        var userRepository =
            context.HttpContext.RequestServices.GetRequiredService<IGettableUserRepository>();

        Int64 userId = tokenReadService.GetUserId(token!);
        User? user = userRepository.GetByIdIncludeAll(userId);
        if (user == null || user.Blocked)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }
        var ipAddress = context.HttpContext.Request.Headers["X-Forwarded-For"];
        if (ipAddress.IsNullOrEmpty())
        {
            ipAddress = context.HttpContext.Connection.RemoteIpAddress!.ToString();
        }

        var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
        var ip = user.Devices.FirstOrDefault(d => d.Ip == ipAddress && d.IpBlocked);
        if (ip != null)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }
        Console.WriteLine($"IP: {ipAddress} | USERAGENT: {userAgent}");
        var device = user.Devices.FirstOrDefault(d =>
            d.Ip == ipAddress && d.UserAgent == userAgent
        );
        if (device == null)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }
        if (user.ClaimDeviceVerification && !device.Verified)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }
        if (device.Blocked)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        context.HttpContext.Items["Device"] = device;
        dataAccessor.Device = device;
        context.HttpContext.Items["User"] = user;
        dataAccessor.User = user;

        base.OnActionExecuting(context);
    }
}
