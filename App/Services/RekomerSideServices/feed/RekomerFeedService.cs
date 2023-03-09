using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFeedService : IRekomerFeedService
{
   public Task<IEnumerable<RekomerRestaurantDetailResponseDto>> GetFeeds(RekomerGetFeedsRequestDto getRequest)
   {
      throw new NotImplementedException();
   }
}