using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRestaurantService
{
   public Task<RekomerRestaurantDetailDto?> GetRestaurantDetailAsync(string restaurantId);
}