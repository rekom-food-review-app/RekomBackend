namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerFoodDetailResponseDto
{
   public string Id { get; set; } = null!;
   public string PrimaryImage { get; set; } = null!;
   public string Name { get; set; } = null!;
   public float Price { get; set; }
   public IEnumerable<string> Images { get; set; } = null!;
   
   public string RestaurantId { get; set; } = null!;
   public string RestaurantName { get; set; } = null!;
   public string RestaurantDescription { get; set; } = null!;
}