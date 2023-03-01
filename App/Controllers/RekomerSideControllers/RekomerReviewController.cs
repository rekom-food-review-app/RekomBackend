using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
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
   public async Task<IActionResult> GetRestaurantReviews(string restaurantId, [FromQuery] int page, [FromQuery] int size)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var reviews = await _reviewService.GetRestaurantReviewListAsync(meId, restaurantId, page, size);

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

   [HttpPost("restaurants/{restaurantId}/reviews")]
   public async Task<IActionResult> CreateReview(string restaurantId, [FromForm] RekomerCreateReviewRequestDto reviewRequest)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _reviewService.CreateReviewAsync(meId, restaurantId, reviewRequest);
         return Ok();
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

   [HttpPost("reviews/{reviewId}/comments")]
   public async Task<IActionResult> Comment(string reviewId, [FromBody] RekomerCommentRequestDto commentRequest)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _reviewService.CommentAsync(meId, reviewId, commentRequest);

         return Ok();
      }
      catch (InvalidAccessTokenException)
      {
         return Unauthorized();
      }
      catch (NotFoundReviewException)
      {
         return NotFound();
      }
   }

   [HttpGet("reviews/{reviewId}/comments")]
   public async Task<IActionResult> GetCommentList(string reviewId, [FromQuery] int page, [FromQuery] int size)
   {
      try
      {
         var commentList = await _reviewService.GetCommentListAsync(reviewId, page, size);

         return Ok(new
         {
            commentList
         });
      }
      catch (NotFoundReviewException)
      {
         return NotFound();
      }
   }
}