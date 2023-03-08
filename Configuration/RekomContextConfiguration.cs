using Microsoft.EntityFrameworkCore;
using RekomBackend.Database;

namespace RekomBackend.Configuration;

public static class RekomContextConfiguration
{
   public static IServiceCollection ConfigRekomContext(this IServiceCollection services, IConfiguration configuration)
   {
      var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

      return services.AddDbContext<RekomContext>(
         options => options.UseMySql(
            configuration.GetValue<string>("MySQLConnectionString")!, 
            serverVersion, 
            optionsBuilder => optionsBuilder.UseNetTopologySuite())
      );
   }
}