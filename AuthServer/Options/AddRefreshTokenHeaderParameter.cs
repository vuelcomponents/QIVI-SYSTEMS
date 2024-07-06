using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace authServer.Options
{
    public class AddRefreshTokenHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "Grant-Type",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema { Type = "string" },
                    Description = "Refresh token",
                    Required = false
                }
            );
        }
    }
}
