using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFollowService
{
   public Task FollowOtherRekomerAsync(string rekomerId);
   
   public Task UnfollowOtherRekomerAsync(string rekomerId);

   public Task<IEnumerable<RekomerProfileResponse?>> GetMyFollowersAsync(int? page = null, int? limit = null);

   public Task<IEnumerable<RekomerProfileResponse?>> GetMyFollowingsAsync(int? page = null, int? limit = null);

   public Task<IEnumerable<RekomerProfileResponse?>> GetOtherFollowersAsync(string rekomerId, int? page = null, int? limit = null);
   
   public Task<IEnumerable<RekomerProfileResponse?>> GetOtherFollowingsAsync(string rekomerId, int? page = null, int? limit = null);
}