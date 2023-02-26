using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerReviewService
{
   public Task<IEnumerable<Review>> GetRestaurantReviewsAsync(string restaurantId);
}