using authServer.Options;
using authServer.Services.MailerService.FactoryMailerService;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace authServer.Services.MailerService;

public interface IMailerChangeEmailService
{
    void SendChangeEmailEmail(User user, string vToken);
}

public class MailerChangeEmailService(IOptions<EmailOptions> options)
    : BaseMailerService(options),
        IMailerChangeEmailService
{
    public void SendChangeEmailEmail(User user, string vToken)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("QUARX", Options.Value.AuthMailAddress));
        message.To.Add(new MailboxAddress(user.FirstName, user.NewEmail));
        message.Subject = "Change email";
        message.Body = new TextPart("plain");

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var templatePath = Path.Combine(basePath, "Resources", "MailTemplate.html");
        var htmlTemplate = File.ReadAllText(templatePath);

        var htmlBody = htmlTemplate
            .Replace("{UserName}", user.FirstName)
            .Replace("{Header}", "Change your email")
            .Replace("{Description}", "You can verify your email by clicking the button below")
            .Replace(
                "{VerificationLink}",
                $"{Urls.GetAssociatedUrl(Urls.Name.AuthServer)}/auth/change-email-key/{Uri.EscapeDataString(vToken)}"
            );

        message.Body = new TextPart("html") { Text = htmlBody };

        using (var client = new SmtpClient())
        {
            client.Connect(Options.Value.AuthMailSmtp, 587, false);
            client.Authenticate(Options.Value.AuthMailAddress, Options.Value.AuthMailPassword);
            client.Send(message);
            client.Disconnect(true);
        }

        message.Body = new TextPart("plain")
        {
            Text =
                $"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/reset-password/{Uri.EscapeDataString(vToken)}"
        };
        using (var client = new SmtpClient())
        {
            client.Connect(Options.Value.AuthMailSmtp, 587, false);
            client.Authenticate(Options.Value.AuthMailAddress, Options.Value.AuthMailPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
