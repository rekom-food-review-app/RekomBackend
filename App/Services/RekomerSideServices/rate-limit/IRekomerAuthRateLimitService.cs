namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerAuthRateLimitService : IRekomerRateLimitService
{
   public Task<bool> IsAllowedAsync(string ipAddress);
}