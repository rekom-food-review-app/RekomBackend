using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRestaurantService
{
   public Task<RekomerRestaurantDetailResponseDto?> GetRestaurantProfileByIdAsync(string restaurantId);
}
