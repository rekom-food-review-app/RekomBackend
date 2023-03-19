using Microsoft.Extensions.Caching.Distributed;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerCreatReviewRateLimit : IRekomerCreatReviewRateLimit
{
   private readonly int _maxRequest;
   private readonly DateTime _expire;
   
   private readonly IDistributedCache _distributedCache;
   
   public RekomerCreatReviewRateLimit(IDistributedCache distributedCache)
   {
      _distributedCache = distributedCache;
      _maxRequest = 3;
      _expire = DateTime.Today.AddDays(1);
   }

   public async Task IncreaseRequestTimeByOne(string meId)
   {
      var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(_expire);
      try
      {
         var requestTime = int.Parse(await _distributedCache.GetStringAsync(meId) ?? string.Empty);
         await _distributedCache.SetStringAsync(meId, (requestTime + 1).ToString(), options);
      }
      catch (FormatException)
      {
         await _distributedCache.SetStringAsync(meId, "1", options);
      }
   }

   public async Task<bool> IsAllowedAsync(string meId)
   {
      try
      {
         var requestTime = int.Parse(await _distributedCache.GetStringAsync(meId) ?? string.Empty);
         return requestTime < _maxRequest;
      }
      catch (FormatException)
      {
         return true;
      }      
   }
}