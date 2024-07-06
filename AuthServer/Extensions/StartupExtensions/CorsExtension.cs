using ClassLibrary.Enums;

namespace authServer.Extensions.StartupExtensions;

public static class CorsExtension
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

    public static IApplicationBuilder RunCors(this IApplicationBuilder app)
    {
        app.UseCors("All");
        return app;
    }
}
