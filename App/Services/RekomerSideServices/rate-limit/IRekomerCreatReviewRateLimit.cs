namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerCreatReviewRateLimit
{
   public Task IncreaseRequestTimeByOne(string meId);

   public Task<bool> IsAllowedAsync(string meId);
}