using ClassLibrary.Dtos.Auth;
using HrTechniqueServer.Infrastructure.Clients;
using HrTechniqueServer.Infrastructure.Filters;
using HrTechniqueServer.Shared.Resources;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Web.Controllers;

[ApiController]
[AuthorizedMicroFilter]
[Route("auth")]
public sealed class AuthController(AuthClient authClient, IAuthResource authResource) : ControllerBase
{
    [HttpGet("authorized")]
    public ActionResult<AuthResponseShort> Authorized()
    {
        try
        {
            return Ok(authResource.AuthResponse);
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await authClient.Call("auth/logout");
        return Ok();
    }

    [HttpGet("notifications")]
    public async Task<IActionResult> Notifications()
    {
        try
        {
            return Ok(await authClient.Get<List<NotificationDto>>("auth/notifications"));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("notifications-seen/{id}")]
    public async Task<ActionResult<List<NotificationDto>>> SeenNotification(long id)
    {
        try
        {
            await authClient.Get<List<NotificationDto>>($"auth/notifications-seen/{id}");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
