using authServer.Options;
using authServer.Services.MailerService.FactoryMailerService;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace authServer.Services.MailerService;

public interface IMailerVerificationService
{
    void SendVerificationEmail(User user, string vToken);
    void SendDeviceVerificationEmail(User user, Int64 deviceId, string dvToken);
}

public class MailerVerificationService(IOptions<EmailOptions> options)
    : BaseMailerService(options),
        IMailerVerificationService
{
    public void SendVerificationEmail(User user, string vToken)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("QUARX", Options.Value.AuthMailAddress));
        message.To.Add(new MailboxAddress(user.FirstName, user.Email));
        message.Subject = "Please confirm your email address";

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var templatePath = Path.Combine(basePath, "Resources", "MailTemplate.html");
        var htmlTemplate = File.ReadAllText(templatePath);

        var htmlBody = htmlTemplate
            .Replace("{UserName}", user.FirstName)
            .Replace("{Header}", "Please confirm your email, QUARX")
            .Replace(
                "{Description}",
                "Please confirm your email address by clicking the button below"
            )
            .Replace(
                "{VerificationLink}",
                $"{Urls.GetAssociatedUrl(Urls.Name.AuthServer)}/auth/verify/{vToken}"
            );

        message.Body = new TextPart("html") { Text = htmlBody };

        using (var client = new SmtpClient())
        {
            client.Connect(Options.Value.AuthMailSmtp, 587, false);
            client.Authenticate(Options.Value.AuthMailAddress, Options.Value.AuthMailPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public void SendDeviceVerificationEmail(User user, Int64 deviceId, string dvToken)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("QUARX", Options.Value.AuthMailAddress));
        message.To.Add(new MailboxAddress(user.FirstName, user.Email));
        message.Subject = "Please confirm your device";

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var templatePath = Path.Combine(basePath, "Resources", "MailTemplate.html");
        var htmlTemplate = File.ReadAllText(templatePath);

        var htmlBody = htmlTemplate
            .Replace("{UserName}", user.FirstName)
            .Replace("{Header}", "Device verification")
            .Replace(
                "{Description}",
                "Please confirm your device address by clicking the button below"
            )
            .Replace(
                "{VerificationLink}",
                $"{Urls.GetAssociatedUrl(Urls.Name.AuthServer)}/auth/device-verify/{deviceId}/{Uri.EscapeDataString(dvToken)}"
            );

        message.Body = new TextPart("html") { Text = htmlBody };

        using (var client = new SmtpClient())
        {
            client.Connect(Options.Value.AuthMailSmtp, 587, false);
            client.Authenticate(Options.Value.AuthMailAddress, Options.Value.AuthMailPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
