using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerSearchService
{
   public Task<IEnumerable<RekomerRestaurantCardResponseDto>> SearchForRestaurantAsync(RekomerSearchRequestDto searchRequest);
   
   public Task<IEnumerable<RekomerFoodInMenuResponseDto>> SearchForFoodAsync(RekomerSearchRequestDto searchRequest);
   
   public Task<IEnumerable<RekomerCardInfoResponseDto>> SearchForRekomerAsync(string meId, RekomerSearchRequestDto searchRequest);
   
   public Task<RekomerSearchForAllResponse> SearchForAllAsync(string meId, RekomerSearchRequestDto searchRequest);
}