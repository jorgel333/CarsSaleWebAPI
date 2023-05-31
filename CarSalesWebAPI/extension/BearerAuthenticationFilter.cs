using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IMDb_api.Extensions
{
    public class BearerAthenticationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var allowAnonimos = context.ApiDescription.CustomAttributes().Any(atrib => atrib.GetType() == typeof(AllowAnonymousAttribute));
            var athorize = context.ApiDescription.CustomAttributes().Any(atrib => atrib.GetType() == typeof(AuthorizeAttribute));

            if (allowAnonimos || !athorize) return;

            operation.Security = new List<OpenApiSecurityRequirement>()
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                }
            };
        }
    }
}