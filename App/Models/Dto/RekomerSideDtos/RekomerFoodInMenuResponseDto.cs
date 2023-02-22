namespace RekomBackend.App.Models.Dto.RekomerSideDtos;

public class RekomerFoodInMenuResponseDto
{
   public string Name { get; set; } = null!;
   
   public float Price { get; set; }
   
   public string ImageUrl { get; set; } = null!;
}