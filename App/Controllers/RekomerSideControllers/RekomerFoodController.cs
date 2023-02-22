using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/foods")]
[Authorize(Roles = "Rekomer")]
public class RekomerFoodController : ControllerBase
{
   private readonly IRekomerFoodService _foodService;

   public RekomerFoodController(IRekomerFoodService foodService)
   {
      _foodService = foodService;
   }

   [HttpGet]
   public async Task<IActionResult> GetFoodsInMenu([FromQuery, Required] string restaurantId)
   {
      try
      {
         var foodList = await _foodService.GetFoodsInMenuAsync(restaurantId);
         return Ok(new
         {
            foodList
         });
      }
      catch (NotFoundRestaurantException e)
      {
         return NotFound();
      }
   }
}