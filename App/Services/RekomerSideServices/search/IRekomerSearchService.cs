using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerSearchService
{
   public Task<IEnumerable<RekomerRestaurantCardResponseDto>> SearchForRestaurant(RekomerSearchForRestaurantRequestDto searchRequest);
}