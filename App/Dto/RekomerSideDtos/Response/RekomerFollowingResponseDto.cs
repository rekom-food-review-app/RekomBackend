namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerFollowingResponseDto
{
   public string RekomerId { get; set; } = null!;

   public string RekomerAvatarUrl { get; set; } = null!;

   public string RekomerFullName { get; set; } = null!;

   public string RekomerDescription { get; set; } = null!;
   
   public DateTime CreatedAt { get; set; }
}