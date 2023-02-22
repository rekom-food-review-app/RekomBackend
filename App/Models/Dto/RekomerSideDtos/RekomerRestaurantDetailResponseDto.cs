namespace RekomBackend.App.Models.Dto.RekomerSideDtos;

public class RekomerRestaurantDetailResponseDto : RatingResultDto
{
   #region BasicInfo

   public string Id { get; set; } = null!;

   public string Name { get; set; } = null!;

   public string CoverImageUrl { get; set; } = null!;

   public string Description { get; set; } = null!;
   
   public string Coordinates { get; set; } = null!;

   #endregion

   #region Award

   // public IEnumerable<Award> Awards { get; set; } = null!;

   #endregion
}