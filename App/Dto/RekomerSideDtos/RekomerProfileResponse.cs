namespace RekomBackend.App.Dto.RekomerSideDtos;

public class RekomerProfileResponse
{
   public string Id { get; set; } = null!;
   
   public string Username { get; set; } = null!;

   public string AvatarUrl { get; set; } = null!;

   public string FullName { get; set; } = null!;
   
   public string Description { get; set; } = null!;

   public bool? IsFollowed { get; set; }

   public string TotalFollowers { get; set; } = "0";

   public string TotalFollowings { get; set; } = "0";

   public string TotalReviews { get; set; } = "0";
}