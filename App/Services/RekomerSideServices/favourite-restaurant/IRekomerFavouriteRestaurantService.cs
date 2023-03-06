namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFavouriteRestaurantService
{
   public Task AddAsync(string meId, string restaurantId);
}