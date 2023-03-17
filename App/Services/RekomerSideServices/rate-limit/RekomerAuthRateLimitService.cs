using Microsoft.Extensions.Caching.Distributed;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerAuthRateLimitService : IRekomerAuthRateLimitService
{
   private readonly int _maxRequest;
   private readonly DateTime _expire;
   
   private readonly IDistributedCache _distributedCache;
   
   public RekomerAuthRateLimitService(IDistributedCache distributedCache)
   {
      _distributedCache = distributedCache;
      _maxRequest = 3;
      _expire = DateTime.Today.AddDays(1);
   }

   public async Task<bool> IsAllowedAsync(string ipAddress)
   {
      var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(_expire);
      try
      {
         var requestTime = int.Parse(await _distributedCache.GetStringAsync(ipAddress) ?? string.Empty);

         if (requestTime >= _maxRequest) return false;
         await _distributedCache.SetStringAsync(ipAddress, (requestTime + 1).ToString(), options);
         return true;
      }
      catch (FormatException) // in case request time is null
      {
         await _distributedCache.SetStringAsync(ipAddress, "1", options);
         return true;
      }
   }
}