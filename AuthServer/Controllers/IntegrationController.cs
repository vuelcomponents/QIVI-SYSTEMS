using authServer.Attributes;
using authServer.Data;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authServer.Controllers;

[ApiController]
[Authorize]
[BlockedTokens]
[AuthDevice]
[VerifiedEmail]
[Route("integration")]
public class IntegrationController(AuthDataContext authDataContext, IMapper mapper) : ControllerBase
{
    [HttpGet("get-user-by-id/{userId}")]
    public async Task<ActionResult<UserShortDto>> GetUserById(Int64 userId)
    {
        try
        {
            return Ok(
                mapper.Map<UserDto>(
                    await authDataContext.Users.FirstOrDefaultAsync(u => u.Id == userId)
                )
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
