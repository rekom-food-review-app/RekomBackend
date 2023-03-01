namespace RekomBackend.Configuration;

public static class ServiceCollectionConfiguration
{
   public static void ConfigServiceCollection(this IServiceCollection services, IConfiguration configuration)
   {
      services
      .AddControllers()
      .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
      
      services.AddEndpointsApiExplorer();
      services.ConfigSwagger();
      
      services.AddHttpContextAccessor();

      services.ConfigJwtAuth(configuration);

      services.ConfigRekomContext(configuration);
      services.InjectDependencies();
      services.ConfigSettingOption(configuration);

      services.AddAutoMapper(typeof(Program).Assembly);
      
      services.AddHttpContextAccessor();

      services.ConfigAuthorization();
      
      services.ConfigCors();

      services.AddSignalR();
   }
}