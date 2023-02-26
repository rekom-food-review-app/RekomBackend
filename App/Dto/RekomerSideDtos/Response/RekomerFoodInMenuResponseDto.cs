namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerFoodInMenuResponseDto
{
   public string Name { get; set; } = null!;
   
   public float Price { get; set; }
   
   public string ImageUrl { get; set; } = null!;

   public string RestaurantId { get; set; } = null!;
}