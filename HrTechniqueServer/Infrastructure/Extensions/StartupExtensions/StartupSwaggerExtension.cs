using Microsoft.OpenApi.Models;

namespace HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;

public static class StartupSwaggerExtension
{
    public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
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
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new string[] { }
                    }
                }
            );
        });

        return services;
    }

    public static IApplicationBuilder SetSwagger(
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
