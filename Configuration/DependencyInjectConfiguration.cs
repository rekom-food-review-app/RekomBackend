using RekomBackend.App.Helpers;
using RekomBackend.App.Services.RekomerSideServices;
using RekomBackend.App.Services.RekomerSideServices.account;

namespace RekomBackend.Configuration;

public static class DependencyInjectConfiguration
{
   public static IServiceCollection InjectDependencies(this IServiceCollection services)
   {
      services
      .InjectRepositories()
      .InjectServices()
      .InjectHelpers();
      
      return services;
   }
   
   private static IServiceCollection InjectRepositories(this IServiceCollection services)
   {
      return services;
   }
   
   private static IServiceCollection InjectServices(this IServiceCollection services)
   {
      services.AddScoped<IRekomerRegisterService, RekomerRegisterService>();
      services.AddScoped<IRekomerOtpService, RekomerOtpService>();
      services.AddScoped<IRekomerMailService, RekomerMailService>();
      services.AddScoped<IRekomerAuthService, RekomerAuthService>();
      services.AddScoped<IRekomerAccountService, RekomerAccountService>();
      services.AddScoped<IRekomerProfileService, RekomerProfileService>();
      services.AddScoped<IRekomerFollowService, RekomerFollowService>();
      
      return services;
   }

   private static IServiceCollection InjectHelpers(this IServiceCollection services)
   {
      services.AddScoped<IMailHelper, MailHelper>();
      services.AddScoped<IJwtHelper, JwtHelper>();
      services.AddScoped<IS3Helper, S3Helper>();
      return services;
   }
}