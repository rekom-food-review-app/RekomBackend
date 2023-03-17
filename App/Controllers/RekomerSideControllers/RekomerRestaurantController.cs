using System.Security.Claims;
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
   private readonly IHttpContextAccessor _httpContextAccessor;
   
   public RekomerRestaurantController(IRekomerRestaurantService restaurantService, IHttpContextAccessor httpContextAccessor)
   {
      _restaurantService = restaurantService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpGet("{restaurantId}")]
   public async Task<IActionResult> GetRestaurantDetail(string restaurantId)
   {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      var restaurant = await _restaurantService.GetRestaurantDetailAsync(meId, restaurantId);

      if (restaurant is null) return NotFound();

      return Ok(new
      {
         code = "SUC",
         messgage = "Found Restaurant",
         restaurant
      });
   }
   
   [HttpGet("{restaurantId}/gallery")]
   public async Task<IActionResult> GetRestaurantGallery(string restaurantId, [FromQuery] int page, [FromQuery] int size)
   {
      try
      {
         var gallery = await _restaurantService.GetRestaurantGalleryAsync(restaurantId, page, size);
      
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