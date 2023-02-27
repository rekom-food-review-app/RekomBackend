using System.Security.Claims;
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
   private readonly IHttpContextAccessor _httpContextAccessor;
   
   public RekomerReviewController(IRekomerReviewService reviewService, IHttpContextAccessor httpContextAccessor)
   {
      _reviewService = reviewService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpGet("restaurants/{restaurantId}/reviews")]
   public async Task<IActionResult> GetRestaurantReviews(string restaurantId)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var reviews = await _reviewService.GetRestaurantReviewsAsync(meId, restaurantId);

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

   [HttpGet("reviews/{reviewId}")]
   public async Task<IActionResult> GetReviewDetail(string reviewId)
   {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      var review = await _reviewService.GetReviewDetailAsync(meId, reviewId);

      if (review is null)
      {
         return NotFound();
      }

      return Ok(new
      {
         review
      });
   }
}