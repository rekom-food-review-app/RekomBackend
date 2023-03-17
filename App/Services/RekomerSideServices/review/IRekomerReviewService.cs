using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerReviewService
{
   public Task<IEnumerable<RekomerReviewCardResponseDto>> GetReviewListByRestaurantAsync(string meId, string restaurantId, int page, int size, DateTime? lastTimestamp);

   public Task<RekomerReviewCardResponseDto?> GetReviewDetailAsync(string meId, string reviewId);

   public Task CommentAsync(string meId, string reviewId, RekomerCommentRequestDto commentRequest);
   
   public Task<IEnumerable<RekomerCommentResponseDto>> GetCommentListAsync(string reviewId, int page, int size, DateTime? lastTimestamp);

   public Task<RekomerReviewCardResponseDto> CreateReviewAsync(string meId, string restaurantId, RekomerCreateReviewRequestDto reviewRequest);

   public Task<IEnumerable<RekomerReactionResponseDto>> GetReactionListAsync(string reviewId, string reactionId,  int page, int size, DateTime? lastTimestamp = null);
   
   public Task ReactToReviewAsync(string meId, string reviewId, string reactionId);
   
   public Task UnReactToReviewAsync(string meId, string reviewId, string reactionId);
   
   public Task<IEnumerable<RekomerReviewCardResponseDto>> GetReviewListByRekomerAsync(string meId, string rekomerId, int page, int size, DateTime? lastTimestamp);
}