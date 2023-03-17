using RekomBackend.App.Entities;
using RekomBackend.App.Helpers;

namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerRestaurantDetailResponseDto
{
   public string Id { get; set; } = null!;
   
   public string CoverImageUrl { get; set; } = null!;

   public string Name { get; set; } = null!;

   public Coordinates Coordinates { get; set; } = null!;
   
   public string Description { get; set; } = null!;
   
   public string Address { get; set; } = null!;
   
   public RatingResultView RatingResult { get; set; } = null!;

   public bool IsMyFav { get; set; }

   public bool CanReview { get; set; }
}