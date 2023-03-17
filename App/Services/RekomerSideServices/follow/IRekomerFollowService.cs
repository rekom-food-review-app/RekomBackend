using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFollowService
{
   public Task FollowAsync(string meId, string strangerId);
   
   public Task UnFollowAsync(string meId, string followingId);

   public Task<IEnumerable<RekomerFollowerResponseDto>> GetFollowerListByRekomerAsync(string rekomerId, int page, int size, DateTime? lastTimestamp = null);
   
   public Task<IEnumerable<RekomerFollowingResponseDto>> GetFollowingListByRekomerAsync(string rekomerId, int page, int size, DateTime? lastTimestamp = null);
}