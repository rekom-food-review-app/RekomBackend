using RekomBackend.App.Models.Dto.RekomerSideDtos;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerReviewService
{
   public Task<IEnumerable<RekomerReviewCardResponseDto>> GetReviewCardsByRestaurantAsync(string restaurantId, int? page = null, int? limit = null);
}