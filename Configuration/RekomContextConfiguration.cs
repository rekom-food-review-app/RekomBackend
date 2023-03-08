using Microsoft.EntityFrameworkCore;
using RekomBackend.Database;

namespace RekomBackend.Configuration;

public static class RekomContextConfiguration
{
   public static IServiceCollection ConfigRekomContext(this IServiceCollection services, IConfiguration configuration)
   {
      return services.AddDbContext<RekomContext>(
         options => options.UseNpgsql(
            configuration.GetValue<string>("PostgresConnectionString")!,
            o => o.UseNetTopologySuite())
      );
   }
}