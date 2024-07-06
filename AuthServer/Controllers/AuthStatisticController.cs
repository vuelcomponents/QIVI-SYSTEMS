using authServer.Attributes;
using authServer.Services;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authServer.Controllers;

[ApiController]
[Route("stat")]
[Authorize]
[BlockedTokens]
[Admin]
[AuthDevice]
[VerifiedEmail]
public class AuthStatisticController(IAuthStatisticService authStatisticService) : ControllerBase
{
    [HttpGet("get")]
    public ActionResult<StatisticDto> GetStats()
    {
        try
        {
            return Ok(authStatisticService.GetStats((HttpContext.Items["User"] as User)!));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
