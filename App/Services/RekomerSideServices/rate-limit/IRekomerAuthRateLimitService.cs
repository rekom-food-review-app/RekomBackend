namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerAuthRateLimitService
{
   public Task<bool> IsAllowedAsync(string ipAddress);
}