using authServer.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace authServer.Extensions.StartupExtensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AddRefreshTokenHeaderParameter>();
            options.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    Description =
                        "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
                    In = ParameterLocation.Cookie,
                    Name = "hrtechniquetoken",
                    Type = SecuritySchemeType.ApiKey
                }
            );
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }

    public static IApplicationBuilder RunSwagger(
        this IApplicationBuilder app,
        IWebHostEnvironment env
    )
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}
