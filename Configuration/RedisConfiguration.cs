namespace RekomBackend.Configuration;

public static class RedisConfiguration
{
   public static IServiceCollection ConfigRedis(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddStackExchangeRedisCache(redisOptions =>
      {
         var connectionString = configuration.GetValue<string>("RedisConnectionString")!;
         redisOptions.Configuration = connectionString;
      });
      return services;
   }
}