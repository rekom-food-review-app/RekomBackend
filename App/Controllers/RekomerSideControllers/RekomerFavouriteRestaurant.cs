using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/favourite-restaurants")]
[Authorize(Roles = "Rekomer")]
public class RekomerFavouriteRestaurant : ControllerBase
{
   private readonly IRekomerFavouriteRestaurantService _favouriteRestaurantService;
   private readonly IHttpContextAccessor _httpContextAccessor;

   public RekomerFavouriteRestaurant(IRekomerFavouriteRestaurantService favouriteRestaurantService, IHttpContextAccessor httpContextAccessor)
   {
      _favouriteRestaurantService = favouriteRestaurantService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpPost]
   public async Task<IActionResult> AddToFavouriteList([FromBody] AddToFavouriteListRequestDto addRequest)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _favouriteRestaurantService.AddAsync(meId, addRequest.RestaurantId);

         return StatusCode(201);
      }
      catch (NotFoundRekomerException)
      {
         return Unauthorized();
      }
      catch (NotFoundRestaurantException)
      {
         return NotFound();
      }
      catch (RestaurantAlreadyInFavouriteListException)
      {
         return BadRequest();
      }
   }
}