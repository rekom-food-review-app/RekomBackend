using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFeedService : IRekomerFeedService
{
   public Task<IEnumerable<RekomerFeedResponseDto>> GetFeedsAsync(RekomerGetFeedRequestDto getFeedRequest)
   {
      throw new NotImplementedException();
   }
}