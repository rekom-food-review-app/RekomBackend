using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerReviewService
{
   public Task<IEnumerable<RekomerReviewCardResponseDto>> GetRestaurantReviewsAsync(string restaurantId);

   public Task<RekomerReviewCardResponseDto> GetReviewDetail(string reviewId);
}