using authServer.Options;
using Microsoft.Extensions.Options;

namespace authServer.Services.MailerService.FactoryMailerService;

public interface IFactoryMailerService
{
    IMailerChangeEmailService CreateMailerChangeEmailService();
    IMailerResetPasswordService CreateMailerResetPasswordService();
    IMailerVerificationService CreateMailerVerificationService();
}

public class FactoryMailerService(IOptions<EmailOptions> options)
    : BaseMailerService(options),
        IFactoryMailerService
{
    public IMailerChangeEmailService CreateMailerChangeEmailService()
    {
        return new MailerChangeEmailService(Options);
    }

    public IMailerResetPasswordService CreateMailerResetPasswordService()
    {
        return new MailerResetPasswordService(Options);
    }

    public IMailerVerificationService CreateMailerVerificationService()
    {
        return new MailerVerificationService(Options);
    }
}
