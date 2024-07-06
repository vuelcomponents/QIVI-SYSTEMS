using Xunit;

namespace authServer.Tests;

public class AuthMailerTest : TestConfiguration.TestConfiguration
{
    [Fact]
    public void SendVerificationEmail_IsSuccessful()
    {
        TestServices.TestMailerService.MailerVerificationService.SendVerificationEmail(
            TestEntities.OwnedUser,
            "token"
        );
    }

    [Fact]
    public void SendDeviceVerificationEmail_IsSuccessful()
    {
        TestServices.TestMailerService.MailerVerificationService.SendDeviceVerificationEmail(
            TestEntities.OwnedUser,
            12,
            "token"
        );
    }

    [Fact]
    public void SendResetPassword_IsSuccessful()
    {
        TestServices.TestMailerService.MailerResetPasswordService.SendResetPassword(
            TestEntities.OwnedUser,
            "resetpasswordtoken",
            false
        );
    }

    [Fact]
    public void SendChangeEmailEmail_IsSuccessful()
    {
        TestEntities.OwnedUser.NewEmail = "tcmworkouts@gmail.com";
        TestServices.TestMailerService.MailerChangeEmailService.SendChangeEmailEmail(
            TestEntities.OwnedUser,
            "changeemailtoken"
        );
    }
}
