using RekomBackend.App.Entities;

namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerRestaurantDetailResponseDto
{
   public string CoverImageUrl { get; set; } = null!;

   public string Name { get; set; } = null!;

   public string Coordinates { get; set; } = null!;
   
   public string Description { get; set; } = null!;

   public RatingResultView RatingResult { get; set; } = null!;

   public bool IsMyFav { get; set; }
}