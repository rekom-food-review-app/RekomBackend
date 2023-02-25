namespace RekomBackend.App.Dto.RekomerSideDtos;

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

   public string ReviewTime { get; set; } = null!;

   #endregion

   #region Reaction

   public string AmountDislike { get; set; } = "0";

   public string AmountLike { get; set; } = "0";

   public string AmountHelpful { get; set; } = "0";

   public string AmountComment { get; set; } = "0";

   public string? MyReaction { get; set; }

   #endregion

   #region Restaurant

   public string RestaurantId { get; set; } = null!;

   public string RestaurantName { get; set; } = null!;

   #endregion
}