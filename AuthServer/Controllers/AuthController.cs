using System.Security.Claims;
using authServer.Attributes;
using authServer.Exceptions;
using authServer.Services.AuthService;
using authServer.Services.DeviceService;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = authServer.Services.AuthService.IAuthorizationService;

namespace authServer.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(
        IAuthorizationService authorizationService,
        IAuthUserService authUserService,
        IChangeEmailService changeEmailService,
        IChangePasswordService changePasswordService,
        ILoginService loginService,
        ILogoutService logoutService,
        IRegisterSuperAdminService registerSuperAdminService,
        IResetPasswordService resetPasswordService,
        IVerificationService verificationService,
        IDeviceBlockService deviceBlockService,
        IDeviceRetrievalService deviceRetrievalService,
        IDeviceSignoutService deviceSignoutService,
        IDeviceUnlockService deviceUnlockService,
        IDeviceVerificationService deviceVerificationService,
        IIpBlockService ipBlockService,
        IIpUnlockService ipUnlockService,
        IAuthDataAccessor authDataAccessor
    ) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("verify/{verificationToken}")]
        public async Task<ActionResult<AuthResponse>> VerifyEmail(string verificationToken)
        {
            try
            {
                string resetPasswordToken = await verificationService.VerifyEmail(
                    verificationToken
                );
                return Redirect(
                    $"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/reset-password/{resetPasswordToken}"
                );
            }
            catch (Exception e)
            {
                return Redirect($"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/?err={e.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("device-verify/{deviceId}/{deviceVerificationToken}")]
        public async Task<ActionResult<AuthResponse>> VeriyDevice(
            Int64 deviceId,
            string deviceVerificationToken
        )
        {
            try
            {
                await deviceVerificationService.VerifyDeviceFromMailLink(
                    deviceId,
                    deviceVerificationToken
                );
                return Redirect(
                    $"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/?inf=deviceVerified"
                );
            }
            catch (Exception e)
            {
                return Redirect($"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/?err={e.Message}");
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("device-verify-grid")]
        public async Task<ActionResult<DeviceDto>> VeriyDeviceGrid(List<IdDto> devices)
        {
            try
            {
                await deviceVerificationService.InitVerifyDeviceUser(
                    authDataAccessor.User!,
                    devices
                );
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(EmailPasswordDto user)
        {
            try
            {
                var ipAddress = Request.HttpContext.Connection.RemoteIpAddress!.ToString();
                var userAgent = Request.Headers["User-Agent"].ToString();
                return Ok(await loginService.Login(user, ipAddress, userAgent));
            }
            catch (NotifyingException ex)
            {
                return StatusCode(303, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("reset-password")]
        public ActionResult ResetPassword([FromQuery] string email)
        {
            try
            {
                resetPasswordService.InitResetPassword(email, false, out _, true);
                return Ok("checkYourEmailForFurtherSteps");
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

        [AllowAnonymous]
        [HttpGet("verify-reset-password-key")]
        public ActionResult VerifyResetPasswordKey([FromQuery] string t)
        {
            try
            {
                resetPasswordService.VerifyResetPasswordKey(t, out _);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password-by-key")]
        public ActionResult ResetPasswordByKey(ResetPasswordDto resetPassword)
        {
            try
            {
                resetPasswordService.ResetPasswordByKey(
                    resetPassword.Token,
                    resetPassword.NewPassword
                );
                return Ok("yourPasswordHasBeenChangedSignIn");
            }
            catch (Exception e)
            {
                return Redirect($"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/?err={e.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("register-super-admin")]
        public async Task<ActionResult<UserDto>> RegisterSuperAdmin(UserDto user)
        {
            try
            {
                return Ok(await registerSuperAdminService.RegisterSuperAdmin(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("authorized")]
        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        public async Task<ActionResult<AuthResponse>> Authorized()
        {
            try
            {
                return Ok(
                    await authorizationService.Authorized(
                        HttpContext.Request.Cookies["hrtechniquetoken"]!,
                        Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                    )
                );
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpGet("device-signout/{deviceId}")]
        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        public async Task<ActionResult<DeviceDto>> DeviceSignout(Int64 deviceId)
        {
            try
            {
                return Ok(
                    await deviceSignoutService.DeviceSignout(authDataAccessor.User!, deviceId, null)
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPatch("update")]
        public async Task<ActionResult<UserDto>> UpdateSelf(UserUpdateDto user)
        {
            try
            {
                return Ok(await authUserService.UpdateSelf(authDataAccessor.User!, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("change-email")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailDto changeEmail)
        {
            try
            {
                await changeEmailService.InitChangeEmail(authDataAccessor.User!, changeEmail);
                return Ok();
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

        [AllowAnonymous]
        [HttpGet("change-email-key/{ceToken}")]
        public ActionResult ChangeEmailKey(string ceToken)
        {
            try
            {
                changeEmailService.ChangeEmailKey(ceToken);
                return Redirect(
                    $"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/?inf=emailHasBeenChanged"
                );
            }
            catch (Exception e)
            {
                return Redirect($"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/?err={e.Message}");
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("change-password")]
        public async Task<ActionResult<UserDto>> ChangePassword(NewPasswordDto newPassword)
        {
            try
            {
                return Ok(
                    await changePasswordService.ChangePassword(
                        Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                        newPassword
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpGet("get")]
        public async Task<ActionResult<UserDto>> GetSelf()
        {
            try
            {
                return Ok(
                    await authUserService.GetSelf(
                        Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpGet("devices")]
        public ActionResult<List<DeviceDto>> GetDevices()
        {
            try
            {
                return Ok(deviceRetrievalService.GetDevices(authDataAccessor.User!, null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await logoutService.Logout(HttpContext.Request.Cookies);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("device-block")]
        public async Task<ActionResult<List<DeviceDto>>> BlockDevice(List<IdDto> devices)
        {
            try
            {
                return Ok(
                    await deviceBlockService.DeviceBlock(
                        authDataAccessor.User!,
                        devices,
                        authDataAccessor.Device!,
                        null
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("device-unlock")]
        public async Task<ActionResult<List<DeviceDto>>> UnlockDevice(List<IdDto> devices)
        {
            try
            {
                return Ok(
                    await deviceUnlockService.DeviceUnlock(authDataAccessor.User!, devices, null)
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("ip-block")]
        public async Task<ActionResult<List<DeviceDto>>> BlockIp(List<IdDto> devices)
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
                        null
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpPost("ip-unlock")]
        public async Task<ActionResult<List<DeviceDto>>> UnlockIp(List<IdDto> devices)
        {
            try
            {
                return Ok(await ipUnlockService.IpUnlock(authDataAccessor.User!, devices, null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpGet("notifications")]
        public ActionResult<List<NotificationDto>> Notifications()
        {
            try
            {
                return Ok(authUserService.GetNotifications(authDataAccessor.User!));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [BlockedTokens]
        [AuthDevice]
        [VerifiedEmail]
        [HttpGet("notifications-seen/{id}")]
        public async Task<ActionResult<List<NotificationDto>>> SeenNotification(Int64 id)
        {
            try
            {
                return Ok(await authUserService.SeenNotification(authDataAccessor.User!, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("send-verification-to-email/{email}")]
        public async Task<IActionResult> SendVerificationFromDashboard(string email)
        {
            try
            {
                await verificationService.TrySendVerificationToUser(email);
            }
            catch (NotifyingException ex)
            {
                return StatusCode(303, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
    }
}
