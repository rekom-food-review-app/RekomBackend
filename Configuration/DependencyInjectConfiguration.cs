using RekomBackend.App.Helpers;
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
      return services;
   }

   private static IServiceCollection InjectHelpers(this IServiceCollection services)
   {
      services.AddScoped<IMailHelper, MailHelper>();
      return services;
   }
}