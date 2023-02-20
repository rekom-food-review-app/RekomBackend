using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services.rekomer_follow;

public interface IRekomerFollowService
{
   public Task FollowOtherRekomerAsync(string rekomerId);
   
   public Task UnfollowOtherRekomerAsync(string rekomerId);

   public Task<List<Rekomer?>> GetMyFollowers();
}