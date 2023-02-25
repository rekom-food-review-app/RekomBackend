namespace RekomBackend.App.Dto.RekomerSideDtos;

public class RekomerFoodInMenuResponseDto
{
   public string Id { get; set; } = null!;
   
   public string Name { get; set; } = null!;
   
   public float Price { get; set; }
   
   public string ImageUrl { get; set; } = null!;
}