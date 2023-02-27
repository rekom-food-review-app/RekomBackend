using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerReviewService
{
   public Task<IEnumerable<RekomerReviewCardResponseDto>> GetRestaurantReviewsAsync(string meId, string restaurantId);

   public Task<RekomerReviewCardResponseDto?> GetReviewDetailAsync(string meId, string reviewId);
}