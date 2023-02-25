using RekomBackend.App.Helpers;
using RekomBackend.App.Services.CommonService;
using RekomBackend.App.Services.RekomerSideServices;

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
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IAccountService, AccountService>();
      
      services.AddScoped<IRekomerProfileService, RekomerProfileService>();
      services.AddScoped<IRekomerFollowService, RekomerFollowService>();
      
      services.AddScoped<IRekomerRestaurantService, RekomerRestaurantService>();
      services.AddScoped<IRekomerReviewService, RekomerReviewService>();
      services.AddScoped<IRekomerFoodService, RekomerFoodService>();
      services.AddScoped<IRekomerRegisterService, RekomerRegisterService>();
      services.AddScoped<IRekomerOtpService, RekomerOtpService>();
      services.AddScoped<IRekomerMailService, RekomerMailService>();

      
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