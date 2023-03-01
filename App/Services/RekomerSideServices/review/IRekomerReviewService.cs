using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerReviewService
{
   public Task<IEnumerable<RekomerReviewCardResponseDto>> GetRestaurantReviewListAsync(string meId, string restaurantId, int page, int size);

   public Task<RekomerReviewCardResponseDto?> GetReviewDetailAsync(string meId, string reviewId);

   public Task CommentAsync(string meId, string reviewId, RekomerCommentRequestDto commentRequest);
   
   public Task<IEnumerable<RekomerCommentResponseDto>> GetCommentListAsync(string reviewId, int page, int size);

   public Task CreateReviewAsync(string meId, string restaurantId, RekomerCreateReviewRequestDto reviewRequest);
}