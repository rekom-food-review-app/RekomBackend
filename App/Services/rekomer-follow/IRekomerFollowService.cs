using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services;

public interface IRekomerFollowService
{
   public Task FollowOtherRekomerAsync(string rekomerId);
   
   public Task UnfollowOtherRekomerAsync(string rekomerId);

   public Task<List<Rekomer?>> GetMyFollowersAsync(int? page = null, int? limit = null);

   public Task<List<Rekomer?>> GetMyFollowingsAsync(int? page = null, int? limit = null);

   public Task<List<Rekomer?>> GetOtherFollowersAsync(string rekomerId, int? page = null, int? limit = null);
   
   public Task<List<Rekomer?>> GetOtherFollowingsAsync(string rekomerId, int? page = null, int? limit = null);
}