using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side")]
[Authorize(Roles = "Rekomer")]
public class RekomerFoodController : ControllerBase
{
   private readonly IRekomerFoodService _foodService;

   public RekomerFoodController(IRekomerFoodService foodService)
   {
      _foodService = foodService;
   }

   [HttpGet("restaurants/{restaurantId}/foods")]
   public async Task<IActionResult> GetFoodsInMenu(string restaurantId, [FromQuery] int page, [FromQuery] int size)
   {
      try
      {
         var foods = await _foodService.GetFoodsInMenuAsync(restaurantId, page, size);

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
   
   [HttpGet("foods/{foodId}")]
   public async Task<IActionResult> GetFoodDetail(string foodId)
   {
      var food = await _foodService.GetFoodDetailAsync(foodId);

      if (food is null) return NotFound();
      
      return Ok(new
      {
         food
      });
   }
}