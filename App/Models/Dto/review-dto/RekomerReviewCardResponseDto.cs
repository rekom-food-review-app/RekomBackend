namespace RekomBackend.App.Models.Dto;

public class RekomerReviewCardResponseDto
{
   #region Rekomer

   public string RekomerId { get; set; } = null!;

   public string RekomerName { get; set; } = null!;

   public string RekomerAvatarUrl { get; set; } = null!;

   #endregion

   #region Review

   public string ReviewId { get; set; } = null!;
   
   public IEnumerable<RekomerReviewMediaResponseDto> ReviewMedias { get; set; } = null!;

   public string ReviewContent { get; set; } = null!;

   #endregion

   #region Reaction

   public int AmountDislike { get; set; }
   
   public int AmountLike { get; set; }
   
   public int AmountHelpful { get; set; }

   public int AmountComment { get; set; }

   public string? MyReaction { get; set; }

   #endregion

   #region Restaurant

   public string RestaurantId { get; set; } = null!;

   public string RestaurantName { get; set; } = null!;

   #endregion
}