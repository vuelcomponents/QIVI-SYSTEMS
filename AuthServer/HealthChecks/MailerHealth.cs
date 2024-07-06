using authServer.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace authServer.HealthChecks;

public class MailerHealth(IOptions<EmailOptions> emailOptions) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(
                emailOptions.Value.AuthMailSmtp,
                587,
                false,
                cancellationToken
            );
            if (!client.IsConnected)
            {
                return HealthCheckResult.Unhealthy("Could not connect to Mailer.");
            }
            await client.AuthenticateAsync(
                emailOptions.Value.AuthMailAddress,
                emailOptions.Value.AuthMailPassword,
                cancellationToken
            );
            if (!client.IsAuthenticated)
            {
                return HealthCheckResult.Unhealthy("Mailer authentication failed.");
            }
            await client.DisconnectAsync(true, cancellationToken);
            return HealthCheckResult.Healthy("Connected to Mailer successful.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Connection to Mailer is unavailable.", ex);
        }
    }
}
