using ClassLibrary.Dtos.Auth;
using HrTechniqueServer.Infrastructure.Clients;
using HrTechniqueServer.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Web.Controllers;

[ApiController]
[AuthorizedMicro]
[Route("auth")]
public sealed class AuthController(AuthClient authClient) : ControllerBase
{
    [HttpGet("authorized")]
    public ActionResult<AuthResponse> Authorized()
    {
        try
        {
            return Ok(HttpContext.Items["Auth"]);
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
