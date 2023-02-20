using RekomBackend.App.Helpers;
using RekomBackend.App.Helpers.s3;
using RekomBackend.App.Services;

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
      services.AddScoped<IRegisterService, RegisterService>();
      services.AddScoped<IOtpService, OtpService>();
      services.AddScoped<IMailService, MailService>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IAccountService, AccountService>();
      services.AddScoped<IRekomerService, RekomerService>();
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