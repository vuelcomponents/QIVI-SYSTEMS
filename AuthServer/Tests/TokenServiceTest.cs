using Xunit;

namespace authServer.Tests;

public class TokenServiceTest : TestConfiguration.TestConfiguration
{
    [Fact]
    public void WriteToken_OutToken_OutRefreshToken_AreTokensValid()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.OwnedUser,
            out string token,
            out string refreshToken
        );
        bool isTokenVald = TestServices.TestTokenService.TokenValidationService.IsValidToken(token);
        bool isRefreshTokenValid =
            TestServices.TestTokenService.TokenValidationService.IsValidRefreshToken(refreshToken);
        Assert.True(isTokenVald && isRefreshTokenValid);
    }

    [Fact]
    public void WriteVerificationToken_OutToken_IsTokenValid()
    {
        TestServices.TestTokenService.TokenWriteService.WriteVerificationToken(
            TestEntities.OwnedUser,
            out string verificationToken
        );
        bool isVerificationTokenValid =
            TestServices.TestTokenService.TokenValidationService.IsValidVerifyToken(
                verificationToken
            );
        Assert.True(isVerificationTokenValid);
    }

    [Fact]
    public void WriteDeviceVerificationToken_OutToken_IsTokenValid()
    {
        TestServices.TestTokenService.TokenWriteService.WriteDeviceVerificationToken(
            TestEntities.OwnedUser,
            out string deviceVerificationToken
        );
        bool isDeviceVerificationTokenValid =
            TestServices.TestTokenService.TokenValidationService.IsValidDeviceVerificationToken(
                deviceVerificationToken
            );
        Assert.True(isDeviceVerificationTokenValid);
    }

    [Fact]
    public void WriteResetPasswordToken_OutToken_IsTokenValid()
    {
        TestServices.TestTokenService.TokenWriteService.WriteResetPasswordToken(
            TestEntities.OwnedUser,
            out string deviceVerificationToken
        );
        bool isDeviceVerificationTokenValid =
            TestServices.TestTokenService.TokenValidationService.IsValidResetPasswordToken(
                deviceVerificationToken
            );
        Assert.True(isDeviceVerificationTokenValid);
    }

    [Fact]
    public void WriteChangeEmailToken_OutToken_IsTokenValid()
    {
        TestServices.TestTokenService.TokenWriteService.WriteChangeEmailToken(
            TestEntities.OwnedUser,
            out string changeEmailToken
        );
        bool isChangeEmailTokenValid =
            TestServices.TestTokenService.TokenValidationService.IsValidChangeEmailToken(
                changeEmailToken
            );
        Assert.True(isChangeEmailTokenValid);
    }

    [Fact]
    public void GetTimeToExpiration_ReturnsTimeSpan()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.OwnedUser,
            out string token,
            out _
        );
        var result = TestServices.TestTokenService.TokenReadService.GetTimeToExpiration(token);
        Assert.IsType<TimeSpan>(result);
    }

    [Fact]
    public void GetUserId_ReturnsTimeSpan()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.OwnedUser,
            out string token,
            out _
        );
        var result = TestServices.TestTokenService.TokenReadService.GetUserId(token);
        Assert.IsType<Int64>(result);
    }

    [Fact]
    public void IsTokenValid_ReturnsTrue()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.Admin,
            out string token,
            out _
        );
        bool result = TestServices.TestTokenService.TokenValidationService.IsValidToken(token);
        Assert.True(result);
    }

    [Fact]
    public void IsRefreshTokenValid_ReturnsTrue()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.Admin,
            out _,
            out string refreshToken
        );
        bool result = TestServices.TestTokenService.TokenValidationService.IsValidRefreshToken(
            refreshToken
        );
        Assert.True(result);
    }

    [Fact]
    public void IsTokenValid_ReturnsFalse()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.Admin,
            out string token,
            out _
        );
        bool result = TestServices.TestTokenService.TokenValidationService.IsValidToken(
            $"{token}-invalid"
        );
        Assert.False(result);
    }

    [Fact]
    public void IsRefreshTokenValid_ReturnsFalse()
    {
        TestServices.TestTokenService.TokenWriteService.WriteToken(
            TestEntities.Admin,
            out _,
            out string refreshToken
        );
        bool result = TestServices.TestTokenService.TokenValidationService.IsValidRefreshToken(
            $"{refreshToken}-invalid"
        );
        Assert.False(result);
    }

    //
    [Fact]
    public void IsValidDeviceVerificationToken_ReturnsTrue()
    {
        TestServices.TestTokenService.TokenWriteService.WriteDeviceVerificationToken(
            TestEntities.Admin,
            out string token
        );
        bool result =
            TestServices.TestTokenService.TokenValidationService.IsValidDeviceVerificationToken(
                token
            );
        Assert.True(result);
    }

    [Fact]
    public void IsValidVerifyToken_ReturnsTrue()
    {
        TestServices.TestTokenService.TokenWriteService.WriteVerificationToken(
            TestEntities.Admin,
            out string token
        );
        bool result = TestServices.TestTokenService.TokenValidationService.IsValidVerifyToken(
            token
        );
        Assert.True(result);
    }

    [Fact]
    public void IsValidDeviceVerificationToken_ReturnsFalse()
    {
        TestServices.TestTokenService.TokenWriteService.WriteDeviceVerificationToken(
            TestEntities.Admin,
            out string token
        );
        bool result =
            TestServices.TestTokenService.TokenValidationService.IsValidDeviceVerificationToken(
                $"{token}-invalid"
            );
        Assert.False(result);
    }

    [Fact]
    public void IsValidVerifyToken_ReturnsFalse()
    {
        TestServices.TestTokenService.TokenWriteService.WriteVerificationToken(
            TestEntities.Admin,
            out string token
        );
        bool result = TestServices.TestTokenService.TokenValidationService.IsValidVerifyToken(
            $"{token}-invalid"
        );
        Assert.False(result);
    }
}
