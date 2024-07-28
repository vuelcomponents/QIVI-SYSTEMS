using ClassLibrary.Enums;

namespace HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;

public static class StartupCorsExtension
{
    public static IServiceCollection AddAndConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddPolicy(
                name: "All",
                policy =>
                {
                    policy.WithOrigins(
                        [
                            Urls.GetAssociatedUrl(Urls.Name.AuthClient),
                            Urls.GetAssociatedUrl(Urls.Name.AuthServer),
                            Urls.GetAssociatedUrl(Urls.Name.HrTechniqueServer),
                            Urls.GetAssociatedUrl(Urls.Name.HrTechniqueClient),
                        ]
                    );
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                }
            )
        );
        return services;
    }

    public static IApplicationBuilder SetCors(this IApplicationBuilder app)
    {
        app.UseCors("All");
        return app;
    }
}
