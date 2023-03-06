namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerRestaurantCardResponseDto
{
   public string Id { get; set; } = null!;

   public string RestaurantId { get; set; } = null!;

   public string RestaurantName { get; set; } = null!;
   
   public string RestaurantCoverImageUrl { get; set; } = null!;
   
   public float RestaurantRatingAverage { get; set; }

   public DateTime CreatedAt { get; set; }
}