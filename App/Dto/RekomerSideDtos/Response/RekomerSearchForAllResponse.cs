namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerSearchForAllResponse
{
   public IEnumerable<RekomerRestaurantCardResponseDto> RestaurantList { get; set; } = null!;

   public IEnumerable<RekomerFoodInMenuResponseDto> FoodList { get; set; } = null!;

   public IEnumerable<RekomerCardInfoResponseDto> RekomerList { get; set; } = null!;
}