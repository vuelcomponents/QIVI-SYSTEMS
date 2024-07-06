using authServer.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace authServer.HealthChecks;

public class DatabaseHealth(AuthDataContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await dbContext.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy("Database connection is available.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database connection is unavailable.", ex);
        }
    }
}
