using Microsoft.Extensions.Caching.Distributed;

namespace RekomBackend.App.Helpers;

public class RateLimitHelper : IRateLimitHelper
{
   private readonly IDistributedCache _distributedCache;

   public RateLimitHelper(IDistributedCache distributedCache)
   {
      _distributedCache = distributedCache;
   }
}