using authServer.Repositories.UserRepository;
using authServer.Services.TokenService_;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace authServer.Attributes;

public sealed class Admin : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string? token = context.HttpContext.Request.Cookies["hrtechniquetoken"];
        if (token.IsNullOrEmpty())
        {
            context.Result = new ForbidResult();
        }
        else
        {
            var tokenReadService =
                context.HttpContext.RequestServices.GetRequiredService<ITokenReadService>();
            var userRepository =
                context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            Int64 userId = tokenReadService.GetUserId(token!);
            User? user = userRepository.Get(u=> u.Id.Equals(userId), u => u.Users);
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                if (user.Role < Role.ADMIN)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
        base.OnActionExecuting(context);
    }
}
