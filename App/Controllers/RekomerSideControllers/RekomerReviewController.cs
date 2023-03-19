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
   private readonly IRekomerCreatReviewRateLimit _creatReviewRateLimit;
   
   public RekomerReviewController(IRekomerReviewService reviewService, IHttpContextAccessor httpContextAccessor, IRekomerCreatReviewRateLimit creatReviewRateLimit)
   {
      _reviewService = reviewService;
      _httpContextAccessor = httpContextAccessor;
      _creatReviewRateLimit = creatReviewRateLimit;
   }

   [HttpGet("restaurants/{restaurantId}/reviews")]
   public async Task<IActionResult> GetRestaurantReviews(string restaurantId, [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var reviews = await _reviewService.GetReviewListByRestaurantAsync(meId, restaurantId, page, size, lastTimestamp);

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
         var review = await _reviewService.CreateReviewAsync(meId, restaurantId, reviewRequest);
         
         return Ok(new
         {
            review,
            canReview = await _creatReviewRateLimit.IsAllowedAsync(meId) 
         });
      }
      catch (NotFoundRestaurantException)
      {
         return NotFound();
      }
      catch (TooManyRequestException)
      {
         return StatusCode(429);
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
   public async Task<IActionResult> GetCommentList(string reviewId, [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var commentList = await _reviewService.GetCommentListAsync(reviewId, page, size, lastTimestamp);

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

   [HttpGet("reviews/{reviewId}/reactions/{reactionId}")]
   public async Task<IActionResult> GetReactionList(string reviewId, string reactionId, [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var reactionList = await _reviewService.GetReactionListAsync(reviewId, reactionId, page, size, lastTimestamp);

         return Ok(new
         {
            reactionList
         });
      }
      catch (NotFoundReviewException)
      {
         return NotFound();
      }
   }

   [HttpPost("reviews/{reviewId}/reactions/{reactionId}")]
   public async Task<IActionResult> ReactToReview(string reviewId, string reactionId)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _reviewService.ReactToReviewAsync(meId, reviewId, reactionId);

         return Ok();
      }
      catch (InvalidAccessTokenException)
      {
         return Unauthorized();
      }
      catch (NotFoundReactionException)
      {
         return NotFound();
      }
      catch (NotFoundReviewException)
      {
         return NotFound();
      }
   }
   
   [HttpDelete("reviews/{reviewId}/reactions/{reactionId}")]
   public async Task<IActionResult> UnReactToReview(string reviewId, string reactionId)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _reviewService.UnReactToReviewAsync(meId, reviewId, reactionId);

         return Ok();
      }
      catch (InvalidAccessTokenException)
      {
         return Unauthorized();
      }
      catch (NotFoundReactionException)
      {
         return NotFound();
      }
      catch (NotFoundReviewException)
      {
         return NotFound();
      }
      catch (Exception)
      {
         return NotFound();
      }
   }

   [HttpGet("rekomers/me/reviews")]
   public async Task<IActionResult> GetMyReviewList([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var reviews = await _reviewService.GetReviewListByRekomerAsync(meId, meId, page, size, lastTimestamp);

         return Ok(new
         {
            reviews
         });
      }
      catch (NotFoundRekomerException)
      {
         return NotFound();
      }
   }

   [HttpGet("rekomers/{rekomerId}/reviews")]
   public async Task<IActionResult> GetRekomerReviewList(string rekomerId, [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var reviews = await _reviewService.GetReviewListByRekomerAsync(meId, rekomerId, page, size, lastTimestamp);

         return Ok(new
         {
            reviews
         });
      }
      catch (NotFoundRekomerException)
      {
         return NotFound();
      }
   }
}