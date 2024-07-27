using ClassLibrary.Dtos.Auth;
using HrTechniqueServer.Filters;
using HrTechniqueServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Web.Controllers;

[ApiController]
[AuthorizedMicro]
[Route("auth")]
public sealed class AuthController(IAuthServiceConnector authServiceConnector) : ControllerBase
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
        await authServiceConnector.Call("auth/logout");
        return Ok();
    }

    [HttpGet("notifications")]
    public async Task<IActionResult> Notifications()
    {
        try
        {
            return Ok(await authServiceConnector.Get<List<NotificationDto>>("auth/notifications"));
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
            await authServiceConnector.Get<List<NotificationDto>>($"auth/notifications-seen/{id}");
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
