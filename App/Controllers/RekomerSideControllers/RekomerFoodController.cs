using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side")]
public class RekomerFoodController : ControllerBase
{
   private readonly IRekomerFoodService _foodService;

   public RekomerFoodController(IRekomerFoodService foodService)
   {
      _foodService = foodService;
   }

   [HttpGet("restaurants/{restaurantId}/foods")]
   public async Task<IActionResult> GetFoodsInMenu(string restaurantId)
   {
      try
      {
         var foods = await _foodService.GetFoodsInMenuAsync(restaurantId);

         return Ok(new
         {
            foods
         });
      }
      catch (NotFoundRestaurantException)
      {
         return NotFound();
      }
   }
}