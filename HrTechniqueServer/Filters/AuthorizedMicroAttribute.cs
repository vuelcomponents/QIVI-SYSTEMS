using ClassLibrary.Dtos.Auth;
using ClassLibrary.Enums;
using HrTechniqueServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HrTechniqueServer.Filters;

public class AuthorizedMicroAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var authConnector =
            context.HttpContext.RequestServices.GetRequiredService<IAuthServiceConnector>();
        try
        {
            AuthResponseShort userExchange = authConnector.GetSync<AuthResponseShort>(
                "auth/authorized"
            );

            if (userExchange.User == null || userExchange.User.Licences == null)
            {
                context.Result = new UnauthorizedResult();
                base.OnActionExecuting(context);
                return;
            }

            if (
                !userExchange.User.Licences.Any(l =>
                    l.Host.Equals(Urls.GetAssociatedUrl(Urls.Name.HrTechniqueClient))
                )
            )
            {
                context.Result = new UnauthorizedResult();
                base.OnActionExecuting(context);
                return;
            }
            context.HttpContext.Items["Auth"] = userExchange;
            context.HttpContext.Items["User"] = userExchange.User;
        }
        catch (Exception)
        {
            context.Result = new UnauthorizedResult();
        }
        base.OnActionExecuting(context);
    }
}
