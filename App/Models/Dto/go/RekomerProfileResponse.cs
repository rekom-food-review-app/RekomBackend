namespace RekomBackend.App.Models.Dto.go;

public class RekomerProfileResponse
{
   public string Id { get; set; } = null!;
   
   public string Username { get; set; } = null!;

   public string AvatarUrl { get; set; } = null!;

   public string FullName { get; set; } = null!;
   
   public string Description { get; set; } = null!;
}