using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RekomBackend.Configuration;

public static class JwtAuthConfiguration
{
   public static IServiceCollection ConfigJwtAuth(this IServiceCollection services, IConfiguration configuration)
   {
      services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
         options.TokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecret"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
         };
      });
      
      return services;
   }
}