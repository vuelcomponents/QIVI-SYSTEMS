using authServer.Options;
using authServer.Services.MailerService;
using authServer.Services.MailerService.FactoryMailerService;
using Microsoft.Extensions.Options;

namespace authServer.Tests.TestConfiguration.TestServices;

public class TestMailerService
{
    public readonly IMailerChangeEmailService MailerChangeEmailService;
    public readonly IMailerResetPasswordService MailerResetPasswordService;
    public readonly IMailerVerificationService MailerVerificationService;

    public TestMailerService(IOptions<EmailOptions> emailOptions)
    {
        IFactoryMailerService factoryMailerService = new FactoryMailerService(emailOptions);
        MailerChangeEmailService = factoryMailerService.CreateMailerChangeEmailService();
        MailerResetPasswordService = factoryMailerService.CreateMailerResetPasswordService();
        MailerVerificationService = factoryMailerService.CreateMailerVerificationService();
    }
}
