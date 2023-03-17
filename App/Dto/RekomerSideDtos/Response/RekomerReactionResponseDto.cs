namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerReactionResponseDto
{
   public string Id { get; set; } = null!;

   public DateTime CreatedAt { get; set; }

   public string RekomerId { get; set; } = null!;

   public string RekomerName { get; set; } = null!;

   public string RekomerAvatarUrl { get; set; } = null!;
}