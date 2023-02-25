using RekomBackend.App.Dto.RekomerSideDtos;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFoodService
{
   public Task<IEnumerable<RekomerFoodInMenuResponseDto>> GetFoodsInMenuAsync(string restaurantId);
   
   public Task<RekomerFoodDetailResponseDto> GetFoodDetailByIdAsync(string foodId);
}