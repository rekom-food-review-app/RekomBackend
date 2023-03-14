using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRestaurantService
{
   public Task<RekomerRestaurantDetailResponseDto?> GetRestaurantDetailAsync(string meId, string restaurantId);
   
   public Task<IEnumerable<string>> GetRestaurantGalleryAsync(string restaurantId, int page, int size);
   
   public Task<RekomerRestaurantCardResponseDto> GetRestaurantCardAsync(string restaurantId);
}