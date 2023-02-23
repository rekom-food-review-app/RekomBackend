namespace RekomBackend.App.Models.Dto.RekomerSideDtos;

public class RekomerFoodDetailResponseDto
{
   #region Food

   public string FoodId { get; set; } = null!;
   
   public string FoodName { get; set; } = null!;
   
   public float FoodPrice { get; set; }
   
   public string FoodPrimaryImage { get; set; } = null!;

   public IEnumerable<string> FoodImageUrls { get; set; } = null!;

   #endregion

   #region Restaurant

   public string RestaurantId { get; set; } = null!;

   public string RestaurantName { get; set; } = null!;
   
   public string RestaurantDescription { get; set; } = null!;

   #endregion
}