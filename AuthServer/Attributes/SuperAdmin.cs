using authServer.Repositories.UserRepository;
using authServer.Services.TokenService_;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Attributes;

// ReSharper disable once UnusedType.Global
public sealed class SuperAdmin : ActionFilterAttribute
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
        var userRepository =
            context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var dataAccessor =
            context.HttpContext.RequestServices.GetRequiredService<IAuthDataAccessor>();

        Int64 userId = tokenReadService.GetUserId(token!);
        User? user = userRepository.Get(u=>u.Id.Equals(userId));

        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        if (user.Role < Role.SUPERADMIN)
        {
            context.Result = new UnauthorizedResult();
            base.OnActionExecuting(context);
            return;
        }

        context.HttpContext.Items["User"] = user;
        dataAccessor.User = user;

        base.OnActionExecuting(context);
    }
}
