using authServer.Options;
using authServer.Services.MailerService.FactoryMailerService;
using ClassLibrary.Enums;
using ClassLibrary.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace authServer.Services.MailerService;

public interface IMailerResetPasswordService
{
    void SendResetPassword(User user, string vToken, bool firstPassword);
}

public class MailerResetPasswordService(IOptions<EmailOptions> options)
    : BaseMailerService(options),
        IMailerResetPasswordService
{
    public void SendResetPassword(User user, string vToken, bool firstPassword)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("QUARX", Options.Value.AuthMailAddress));
        message.To.Add(new MailboxAddress(user.FirstName, user.Email));
        message.Subject = firstPassword ? "Set new password" : "Password reset";

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var templatePath = Path.Combine(basePath, "Resources", "MailTemplate.html");
        var htmlTemplate = File.ReadAllText(templatePath);

        var htmlBody = htmlTemplate
            .Replace("{UserName}", user.FirstName)
            .Replace("{Header}", firstPassword ? "Set new password" : "Reset your password")
            .Replace("{Description}", "You can set new password by clicking the button below")
            .Replace(
                "{VerificationLink}",
                $"{Urls.GetAssociatedUrl(Urls.Name.AuthClient)}/reset-password/{Uri.EscapeDataString(vToken)}"
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
