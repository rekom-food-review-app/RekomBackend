using RekomBackend.App.Common.Enums;

namespace RekomBackend.Configuration;

public static class AuthorizationConfiguration
{
   public static IServiceCollection ConfigAuthorization(this IServiceCollection services)
   {
      services.AddAuthorization(options =>
      {
         options.AddPolicy(RoleEnum.Rekomer.ToString(), policy => policy.RequireClaim(RoleEnum.Rekomer.ToString()));
         options.AddPolicy(RoleEnum.Restaurant.ToString(), policy => policy.RequireClaim(RoleEnum.Restaurant.ToString()));
         options.AddPolicy(RoleEnum.Admin.ToString(), policy => policy.RequireClaim(RoleEnum.Admin.ToString()));
      });
      
      return services;
   }
}