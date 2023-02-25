using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/restaurants")]
[Authorize(Roles = "Rekomer")]
public class RekomerRestaurantController : ControllerBase
{
   private readonly IRekomerRestaurantService _restaurantService;

   public RekomerRestaurantController(IRekomerRestaurantService restaurantService)
   {
      _restaurantService = restaurantService;
   }

   [HttpGet("{restaurantId}")]
   public async Task<IActionResult> GetRestaurantDetailById(string restaurantId)
   {
      var restaurant = await _restaurantService.GetRestaurantProfileByIdAsync(restaurantId);

      if (restaurant is null)
      {
         return NotFound(new
         {
            code = "NFR",
            message = "Not found"
         });
      }
      
      return Ok(new
      {
         code = "SUC",
         message = "Success",
         restaurant
      });
   }
}