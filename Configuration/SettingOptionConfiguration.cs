using RekomBackend.App.Helpers;

namespace RekomBackend.Configuration;

public static class SettingOptionConfiguration
{
   public static IServiceCollection ConfigSettingOption(this IServiceCollection services, IConfiguration configuration)
   {
      services.Configure<MailHostSetting>(configuration.GetSection(nameof(MailHostSetting)));
      return services;
   }
}