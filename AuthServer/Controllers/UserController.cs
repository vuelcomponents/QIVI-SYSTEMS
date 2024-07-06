using authServer.Attributes;
using authServer.Exceptions;
using authServer.Services.DeviceService;
using authServer.Services.UserService;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authServer.Controllers;

[ApiController]
[Route("user")]
[Authorize]
[BlockedTokens]
[Admin]
[AuthDevice]
[VerifiedEmail]
public class UserController(
    IUserAdminService userAdminService,
    IUserBlockService userBlockService,
    IUserCreateService userCreateService,
    IUserDeleteService userDeleteService,
    IUserNotifyService userNotifyService,
    IUserUpdateService userUpdateService,
    IDeviceBlockService deviceBlockService,
    IDeviceRetrievalService deviceRetrievalService,
    IDeviceUnlockService deviceUnlockService,
    IDeviceVerificationService deviceVerificationService,
    IIpBlockService ipBlockService,
    IIpUnlockService ipUnlockService,
    IAuthDataAccessor authDataAccessor
) : ControllerBase
{
    [HttpGet("get")]
    public ActionResult<List<UserDto>> GetAdminUsers()
    {
        try
        {
            return Ok(userAdminService.GetAdminUsers(authDataAccessor.User!));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("update")]
    public async Task<ActionResult<UserDto>> UpdateAdminUser(UserUpdateDto updateUser)
    {
        try
        {
            return Ok(await userUpdateService.UpdateUser(authDataAccessor.User!, updateUser));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<UserDto>> AddAdminUser(UserDto createUser)
    {
        try
        {
            return Ok(await userCreateService.AddUser(authDataAccessor.User!, createUser));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("delete-many")]
    public async Task<ActionResult<List<UserDto>>> DeleteMany(List<IdDto> users)
    {
        try
        {
            return Ok(await userDeleteService.DeleteMany(authDataAccessor.User!, users));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("devices/{userId}")]
    public ActionResult<List<DeviceDto>> GetDevices(Int64 userId)
    {
        try
        {
            return Ok(deviceRetrievalService.GetDevices(authDataAccessor.User!, userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("device-block/{userId}")]
    public async Task<ActionResult<List<DeviceDto>>> BlockDevice(Int64 userId, List<IdDto> devices)
    {
        try
        {
            return Ok(
                await deviceBlockService.DeviceBlock(
                    authDataAccessor.User!,
                    devices,
                    authDataAccessor.Device!,
                    userId
                )
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("device-unlock/{userId}")]
    public async Task<ActionResult<List<DeviceDto>>> UnlockDevice(Int64 userId, List<IdDto> devices)
    {
        try
        {
            return Ok(
                await deviceUnlockService.DeviceUnlock(authDataAccessor.User!, devices, userId)
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("ip-block/{userId}")]
    public async Task<ActionResult<List<DeviceDto>>> BlockUserIp(Int64 userId, List<IdDto> devices)
    {
        try
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress!.ToString();
            return Ok(
                await ipBlockService.IpBlock(
                    authDataAccessor.User!,
                    devices,
                    authDataAccessor.Device!,
                    ipAddress,
                    userId
                )
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("ip-unlock/{userId}")]
    public async Task<ActionResult<List<DeviceDto>>> UnlockUserIp(Int64 userId, List<IdDto> devices)
    {
        try
        {
            return Ok(await ipUnlockService.IpUnlock(authDataAccessor.User!, devices, userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("device-verify-grid/{userId}")]
    public async Task<ActionResult<List<DeviceDto>>> VeriyDeviceGridAdmin(
        Int64 userId,
        List<IdDto> devices
    )
    {
        try
        {
            return Ok(
                await deviceVerificationService.VerifyDeviceByAdmin(
                    authDataAccessor.User!,
                    userId,
                    devices
                )
            );
        }
        catch (NotifyingException e)
        {
            return StatusCode(303, e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /* __ */
    [HttpPost("notify")]
    public async Task<ActionResult<NotificationDto>> Notify(NotificationRequest request)
    {
        try
        {
            return Ok(await userNotifyService.Notify(request.Users, request.Notification));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /* __ */
    [HttpPost("block")]
    public async Task<ActionResult<List<UserDto>>> Block(List<IdDto> users)
    {
        try
        {
            return Ok(
                await userBlockService.ChangeBlockStatus(authDataAccessor.User!, users, true)
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("unlock")]
    public async Task<ActionResult<List<UserDto>>> Unlock(List<IdDto> users)
    {
        try
        {
            return Ok(
                await userBlockService.ChangeBlockStatus(authDataAccessor.User!, users, false)
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
