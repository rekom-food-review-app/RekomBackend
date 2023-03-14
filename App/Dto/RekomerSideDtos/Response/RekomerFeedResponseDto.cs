namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerFeedResponseDto
{
   public RekomerRestaurantCardResponseDto Restaurant { get; set; } = null!;

   public IEnumerable<RekomerFoodInMenuResponseDto> FoodList { get; set; } = null!;

   public RekomerReviewCardResponseDto StandardReview { get; set; } = null!;
}