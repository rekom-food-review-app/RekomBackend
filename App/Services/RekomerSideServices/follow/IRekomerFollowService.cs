namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFollowService
{
   public Task FollowAsync(string meId, string strangerId);
   
   public Task UnFollowAsync(string meId, string followingId);
}