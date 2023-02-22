using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/reviews")]
[Authorize(Roles = "Rekomer")]
public class RekomerReviewController : ControllerBase
{
   private readonly IRekomerReviewService _reviewService;

   public RekomerReviewController(IRekomerReviewService reviewService)
   {
      _reviewService = reviewService;
   }

   [HttpGet]
   public async Task<IActionResult> GetReviewCardsByRestaurant(
      [FromQuery, Required] string restaurantId, 
      [FromQuery, RegularExpression("^[1-9]\\d*$")] int? page,
      [FromQuery, RegularExpression("^[1-9]\\d*$")] int? limit)
   {
      var reviews = await _reviewService.GetReviewCardsByRestaurantAsync(restaurantId);
      return Ok(new
      {
         reviews
      });
      // throw new NotImplementedException();
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateReview([FromQuery, Required] string restaurantId)
   {
      throw new NotImplementedException();
   }
   
   [HttpGet("{reviewId}")]
   public async Task<IActionResult> GetReviewDetail(string reviewId)
   {
      throw new NotImplementedException();
   }
}