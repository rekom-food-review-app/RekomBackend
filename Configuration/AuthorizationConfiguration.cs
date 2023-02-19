using RekomBackend.App.Common.Enums;

namespace RekomBackend.Configuration;

public static class AuthorizationConfiguration
{
   public static IServiceCollection ConfigAuthorization(this IServiceCollection services)
   {
      services.AddAuthorization(options =>
      {
         options.AddPolicy(Role.Rekomer.ToString(), policy => policy.RequireClaim(Role.Rekomer.ToString()));
         options.AddPolicy(Role.Restaurant.ToString(), policy => policy.RequireClaim(Role.Restaurant.ToString()));
         options.AddPolicy(Role.Admin.ToString(), policy => policy.RequireClaim(Role.Admin.ToString()));
      });
      
      return services;
   }
}