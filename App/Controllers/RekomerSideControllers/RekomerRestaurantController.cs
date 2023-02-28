using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
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
   public async Task<IActionResult> GetRestaurantDetail(string restaurantId)
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
   
   [HttpGet("{restaurantId}/gallery")]
   public async Task<IActionResult> GetRestaurantGallery(string restaurantId)
   {
      try
      {
         var gallery = await _restaurantService.GetRestaurantGalleryAsync(restaurantId);
      
         return Ok(new
         {
            code = "SUC",
            gallery
         });
      }
      catch (NotFoundRestaurantException)
      {
         return NotFound();
      }
   }
}