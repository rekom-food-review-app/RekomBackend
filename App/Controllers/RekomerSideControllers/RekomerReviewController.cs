using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side")]
[Authorize(Roles = "Rekomer")]
public class RekomerReviewController : ControllerBase
{
   private readonly IRekomerReviewService _reviewService;

   public RekomerReviewController(IRekomerReviewService reviewService)
   {
      _reviewService = reviewService;
   }

   [HttpGet("restaurants/{restaurantId}/reviews")]
   public async Task<IActionResult> GetRestaurantReviews(string restaurantId)
   {
      try
      {
         var reviews = await _reviewService.GetRestaurantReviewsAsync(restaurantId);

         return Ok(new
         {
            reviews
         });
      }
      catch (NotFoundRestaurantException)
      {
         return NotFound();
      }
   }
}