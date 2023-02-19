namespace RekomBackend.Configuration;

public static class AuthorizationConfiguration
{
   public static IServiceCollection ConfigAuthorization(this IServiceCollection services)
   {
      services.AddAuthorization(options =>
      {
         options.AddPolicy("rekomer", policy => policy.RequireClaim("rekomer"));
         options.AddPolicy("restaurant", policy => policy.RequireClaim("restaurant"));
         options.AddPolicy("admin", policy => policy.RequireClaim("admin"));
      });
      
      return services;
   }
}