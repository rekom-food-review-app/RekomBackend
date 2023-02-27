using RekomBackend.App.Entities;

namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerReviewCardResponseDto
{
   public string RekomerId { get; set; } = null!;
   public string RekomerFullName{ get; set; } = null!;
   public string RekomerAvatarUrl{ get; set; } = null!;

   public string Id { get; set; }
   public string Content { get; set; } = null!;
   public IEnumerable<string> Images { get; set; } = null!;
   public DateTime CreatedAt { get; set; }
   
   public string RestaurantId { get; set; } = null!;
   public string RestaurantName { get; set; } = null!;

   public string Rating { get; set; } = null!;
   
   public string? MyReaction { get; set; }
   public int AmountAgree { get; set; }
   public int AmountDisagree { get; set; }
   public int AmountUseful { get; set; }
   public int AmountReply { get; set; }
}