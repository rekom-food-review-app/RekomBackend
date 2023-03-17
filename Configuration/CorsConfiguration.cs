namespace RekomBackend.Configuration;

public static class CorsConfiguration
{
   public static IServiceCollection ConfigCors(this IServiceCollection services)
   {
      services.AddCors(options =>
      {
         options.AddPolicy("CorsPolicy", builder =>
         {
            builder
               .WithOrigins("http://ec2-54-178-104-216.ap-northeast-1.compute.amazonaws.com", "RekomProject", "https://github.com/facebook/react-native")
               .AllowAnyHeader()
               .SetIsOriginAllowed(host => true)
               .AllowAnyMethod()
               .AllowCredentials();
         });
      });
      
      return services;
   }
}