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
   public async Task<IActionResult> GetProductDetail(string restaurantId)
   {
      var restaurant = await _restaurantService.GetRestaurantDetailAsync(restaurantId);

      if (restaurant is null) return NotFound();

      return Ok(new
      {
         code = "SUC",
         messgage = "Found Restaurant",
         restaurant
      });
   }
}