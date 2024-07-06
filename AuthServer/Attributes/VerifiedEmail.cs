using authServer.Data;
using authServer.Services;
using authServer.Services.TokenService_;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Attributes;

public sealed class VerifiedEmail : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string? token = context.HttpContext.Request.Cookies["hrtechniquetoken"];
        if (token.IsNullOrEmpty())
        {
            context.Result = new ForbidResult();
            base.OnActionExecuting(context);
            return;
        }
        var tokenReadService =
            context.HttpContext.RequestServices.GetRequiredService<ITokenReadService>();
        var dbContext = context.HttpContext.RequestServices.GetRequiredService<AuthDataContext>();
        Int64 userId = tokenReadService.GetUserId(token!);
        User? user = dbContext.Users.Include(u => u.Users).FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        if (user.Verified == false)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        base.OnActionExecuting(context);
    }
}
