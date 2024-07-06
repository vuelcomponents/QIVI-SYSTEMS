using System.Text;
using authServer.Data;
using authServer.Services.TokenService_;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Attributes;

public sealed class BlockedTokens : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var tokenBlockService =
            context.HttpContext.RequestServices.GetRequiredService<ITokenBlockService>();
        var tokenReadService =
            context.HttpContext.RequestServices.GetRequiredService<ITokenReadService>();
        string? token = context.HttpContext.Request.Cookies["hrtechniquetoken"];

        if (token.IsNullOrEmpty())
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }
        if (tokenBlockService.IsTokenBlocked(token!))
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<AuthDataContext>();
        Int64 userId = tokenReadService.GetUserId(token!);
        User? user = dbContext.Users.Include(u => u.Users).FirstOrDefault(u => u.Id == userId)!;
        if (!user.RefreshToken.IsNullOrEmpty())
        {
            if (
                tokenBlockService.IsTokenRefreshBlocked(Encoding.UTF8.GetString(user.RefreshToken!))
            )
            {
                context.Result = new UnauthorizedResult();
            }
        }

        base.OnActionExecuting(context);
    }
}
