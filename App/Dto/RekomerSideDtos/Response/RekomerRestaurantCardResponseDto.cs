namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerRestaurantCardResponseDto
{
   public string Id { get; set; } = null!;

   public string Name { get; set; } = null!;

   public string CoverImageUrl { get; set; } = null!;

   public float RatingAverage { get; set; }
}