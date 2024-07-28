using ClassLibrary.Dtos.Auth;
using ClassLibrary.Enums;
using HrTechniqueServer.Infrastructure.Clients;
using HrTechniqueServer.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HrTechniqueServer.Infrastructure.Filters;

public class AuthorizedMicroFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var authClient =
            context.HttpContext.RequestServices.GetRequiredService<AuthClient>();
        var authResource =
            context.HttpContext.RequestServices.GetRequiredService<IAuthResource>();
        try
        {
            AuthResponseShort userExchange = authClient.GetSync<AuthResponseShort>(
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

            authResource.AuthResponse = userExchange;
            authResource.User = userExchange.User;
            // context.HttpContext.Items["Auth"] = userExchange;
            // context.HttpContext.Items["User"] = userExchange.User;
        }
        catch (Exception)
        {
            context.Result = new UnauthorizedResult();
        }
        base.OnActionExecuting(context);
    }
}
