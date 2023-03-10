using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFavouriteRestaurantService
{
   public Task AddAsync(string meId, string restaurantId);
   
   public Task DeleteAsync(string meId, string restaurantId);

   public Task<IEnumerable<RekomerFavRestaurantCardResponseDto>> GetMyFavouriteList(string meId, int page, int size, DateTime? lastTimestamp = null);
}