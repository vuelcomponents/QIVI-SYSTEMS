using ClassLibrary.Enums;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace authServer.HealthChecks;

public class FrontendConnectionHealth : IHealthCheck
{
    private readonly RestClient _client =
        new(new RestClientOptions(Urls.GetAssociatedUrl(Urls.Name.AuthClient)));

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await _client.GetAsync(new RestRequest(""), cancellationToken);
            return HealthCheckResult.Healthy("Frontend connection is available.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Frontend connection is unavailable.", ex);
        }
    }
}
