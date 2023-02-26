using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFoodService
{
   public Task<IEnumerable<RekomerFoodInMenuResponseDto>> GetFoodsInMenuAsync(string restaurantId);
   
   public Task<RekomerFoodDetailResponseDto> GetFoodDetail(string foodId);
}