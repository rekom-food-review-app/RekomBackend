using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace RekomBackend.Configuration;

public static class SwaggerConfiguration
{
   public static IServiceCollection ConfigSwagger(this IServiceCollection services)
   {
      services.AddSwaggerGen(options =>
      {
         options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
         {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
         });

         options.OperationFilter<SecurityRequirementsOperationFilter>();
      });
      return services;
   }
}