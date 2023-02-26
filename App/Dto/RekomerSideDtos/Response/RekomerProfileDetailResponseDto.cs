namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerProfileDetailResponseDto
{
   public string Id { get; set; } = null!;

   public string AvatarUrl { get; set; } = null!;

   public string FullName { get; set; } = null!;

   public string? Description { get; set; }

   public string Username { get; set; } = null!;
   
   public bool? IsFollow { get; set; }

   public int AmountReview { get; set; }
   
   public int AmountFollower { get; set; }
   
   public int AmountFollowing { get; set; }
}