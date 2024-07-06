using System.Text;
using authServer.Exceptions;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Enums;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace authServer.Tests;

public class AuthServiceTest : TestConfiguration.TestConfiguration
{
    [Fact]
    public async Task Authorized_Success_ReturnsAuthResponse()
    {
        var mockHttpContext = new Mock<HttpContext>();

        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.Admin,
            out string token,
            out string refreshToken
        );

        var items = new Dictionary<object, object?> { { "Device", TestEntities.AdminDevice } };
        TestEntities.Admin.RefreshToken = Encoding.UTF8.GetBytes(refreshToken);
        mockHttpContext.Setup(m => m.Request.Cookies["hrtechniquetoken"]).Returns($"{token}");
        mockHttpContext.Setup(m => m.Items).Returns(items);

        var result = await TestServices.TestAuthService.AuthorizationService.Authorized(
            mockHttpContext.Object.Request.Cookies["hrtechniquetoken"]!,
            1
        );

        Assert.IsType<AuthResponse>(result);
    }

    [Fact]
    public async Task Authorized_ThrowsSecurityTokenExpiredException_WhenTokenExpired()
    {
        var mockHttpContext = new Mock<HttpContext>();

        TestServices.TestTokenService.TokenWriteService.WriteExpiredTestToken(
            TestEntities.Admin,
            out string token,
            out string refreshToken
        );

        var items = new Dictionary<object, object?> { { "Device", TestEntities.AdminDevice } };

        mockHttpContext.Setup(m => m.Request.Cookies["hrtechniquetoken"]).Returns($"{token}");
        mockHttpContext
            .Setup(m => m.Request.Cookies["hrtechniquetokenrefresh"])
            .Returns(refreshToken);
        mockHttpContext.Setup(m => m.Items).Returns(items);

        await Assert.ThrowsAsync<SecurityTokenExpiredException>(
            () =>
                TestServices.TestAuthService.AuthorizationService.Authorized(
                    mockHttpContext.Object.Request.Cookies["hrtechniquetoken"]!,
                    1
                )
        );
    }

    [Fact]
    public async Task Login_IsSuccessful()
    {
        var emailPassword = new EmailPasswordDto
        {
            Email = TestData.TestUserEmail,
            Password = TestData.TestUserPassword
        };
        var result = await TestServices.TestAuthService.LoginService.Login(
            emailPassword,
            "0.0.0.0",
            "Firefox"
        );
        Assert.IsType<AuthResponse>(result);
    }

    [Fact]
    public async Task Login_ThrowsIncorrectFormDataException()
    {
        var emailPassword = new EmailPasswordDto
        {
            Email = TestData.TestUserEmail,
            Password = "Incorrect_Password"
        };
        await Assert.ThrowsAsync<IncorrectFormDataException>(
            () =>
                TestServices.TestAuthService.LoginService.Login(emailPassword, "0.0.0.0", "Firefox")
        );
    }

    [Fact]
    public async Task Login_ThrowsIpBlockedException()
    {
        var emailPassword = new EmailPasswordDto
        {
            Email = "BlockedIpEmaiL@email.com",
            Password = TestData.TestUserPassword
        };
        await Assert.ThrowsAsync<IpBlockedException>(
            () =>
                TestServices.TestAuthService.LoginService.Login(emailPassword, "0.0.0.0", "Firefox")
        );
    }

    [Fact]
    public async Task Login_ThrowsDeviceBlockedException()
    {
        var emailPassword = new EmailPasswordDto
        {
            Email = "BlockedDeviceEmaiL@email.com",
            Password = TestData.TestUserPassword
        };
        await Assert.ThrowsAsync<DeviceBlockedException>(
            () =>
                TestServices.TestAuthService.LoginService.Login(
                    emailPassword,
                    "1.1.0.0",
                    "BlockedUserAgent"
                )
        );
    }

    [Fact]
    public async Task GetSelf_ReturnsUserDto()
    {
        var result = await TestServices.TestAuthService.AuthUserService.GetSelf(1);
        Assert.IsType<UserDto>(result);
    }

    [Fact]
    public async Task ChangePassword_ReturnsUserDto()
    {
        var newPasswordDto = new NewPasswordDto
        {
            NewPassword = "SomeOfNewPassword@@",
            ReNewPassword = "SomeOfNewPassword@@",
            OldPassword = TestData.TestUserPassword
        };
        var result = await TestServices.TestAuthService.ChangePasswordService.ChangePassword(
            1,
            newPasswordDto
        );
        Assert.IsType<UserDto>(result);
    }

    [Fact]
    public async Task ChangePassword_ThrowsIncorrectFormDataException_WhenIncorrect_OldPassword()
    {
        var newPasswordDto = new NewPasswordDto
        {
            NewPassword = "SomeOfNewPassword@@",
            ReNewPassword = "SomeOfNewPassword@@",
            OldPassword = "IncorrectPassword"
        };
        await Assert.ThrowsAsync<IncorrectFormDataException>(
            () =>
                TestServices.TestAuthService.ChangePasswordService.ChangePassword(1, newPasswordDto)
        );
    }

    [Fact]
    public void InitResetPassword_Successful()
    {
        TestServices.TestAuthService.ResetPasswordService.InitResetPassword(
            TestData.TestUserEmail,
            false,
            out _,
            false
        );
    }

    [Fact]
    public void InitResetPassword_ThrowsNotifyingException_WhenEmailHasBeenAlreadySentInRangeOf15Minutes()
    {
        // first
        TestServices.TestAuthService.ResetPasswordService.InitResetPassword(
            TestData.TestUserEmail,
            false,
            out _,
            false
        );
        // second immedietaly
        Assert.Throws<NotifyingException>(
            () =>
                TestServices.TestAuthService.ResetPasswordService.InitResetPassword(
                    TestData.TestUserEmail,
                    false,
                    out _,
                    false
                )
        );
    }
}
