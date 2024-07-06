using authServer.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace authServer.Extensions.StartupExtensions;

public static class HealthChecksExtension
{
    public static IServiceCollection RegisterHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<DatabaseHealth>("DbContext");
        services.AddHealthChecks().AddCheck<FrontendConnectionHealth>("Frontend");
        services.AddHealthChecks().AddCheck<MailerHealth>("Mailer");
        return services;
    }

    public static IApplicationBuilder RunHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks(
            "/health/client",
            new HealthCheckOptions { Predicate = (check) => check.Name == "Frontend" }
        );
        app.UseHealthChecks(
            "/health/database",
            new HealthCheckOptions { Predicate = (check) => check.Name == "DbContext" }
        );
        app.UseHealthChecks(
            "/health/mailer",
            new HealthCheckOptions { Predicate = (check) => check.Name == "Mailer" }
        );
        return app;
    }
}
