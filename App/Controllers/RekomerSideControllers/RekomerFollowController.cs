using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/rekomers")]
[Authorize(Roles = "Rekomer")]
public class RekomerFollowController : ControllerBase
{
   private readonly IRekomerFollowService _followService;
   private readonly IHttpContextAccessor _httpContextAccessor;

   public RekomerFollowController(IRekomerFollowService followService, IHttpContextAccessor httpContextAccessor)
   {
      _followService = followService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpPost("{strangerId}/follow")]
   public async Task<IActionResult> Follow(string strangerId)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _followService.FollowAsync(meId, strangerId);

         return Ok(new
         {
            code = "FS",
            message = "Follow successfully"
         });
      }
      catch (NotFoundRekomerException)
      {
         return NotFound();
      }
      catch (RekomerFollowYourSelfException)
      {
         return NotFound();
      }
      catch (RekomerAlreadyFollowException)
      {
         return NotFound(new
         {
            code = "AF",
            message = "You already follow this guy"
         });
      }
   }
   
   [HttpDelete("{followingId}/unfollow")]
   public async Task<IActionResult> UnFollow(string followingId)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         await _followService.UnFollowAsync(meId, followingId);

         return Ok(new
         {
            code = "UFS",
            message = "Unfollow successfully"
         });
      }
      catch (NotFoundRekomerException)
      {
         return NotFound();
      }
      catch (RekomerNotAlreadyFollowException)
      {
         return BadRequest(new
         {
            code = "NAF",
            message = "You not already follow this guy"
         });
      }
   }

   [HttpGet("me/followers")]
   public async Task<IActionResult> GetMyFollowerList([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var followerList = await _followService.GetFollowerListByRekomerAsync(meId, page, size, lastTimestamp);
         
         return Ok(new
         {
            followerList
         });
      }
      catch (NotFoundRekomerException)
      {
         return Unauthorized();
      }
   }
   
   // [HttpGet("me/followings")]
   // public async Task<IActionResult> GetMyFollowingList([FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   // {
   //    try
   //    {
   //       var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
   //       var followerList = await _followService.GetFollowingListByRekomerAsync(meId, page, size, lastTimestamp);
   //       
   //       return Ok(new
   //       {
   //          followerList
   //       });
   //    }
   //    catch (NotFoundRekomerException)
   //    {
   //       return Unauthorized();
   //    }
   // }
   //
   [HttpGet("{rekomerId}/followers")]
   public async Task<IActionResult> GetOtherFollowerList(string rekomerId, [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   {
      try
      {
         var followerList = await _followService.GetFollowerListByRekomerAsync(rekomerId, page, size, lastTimestamp);
         
         return Ok(new
         {
            followerList
         });
      }
      catch (NotFoundRekomerException)
      {
         return NotFound();
      }
   }
   
   // [HttpGet("{rekomerId}/followings")]
   // public async Task<IActionResult> GetOtherFollowingList(string rekomerId, [FromQuery] int page, [FromQuery] int size, [FromQuery] DateTime? lastTimestamp)
   // {
   //    try
   //    {
   //       var followerList = await _followService.GetFollowingListByRekomerAsync(rekomerId, page, size, lastTimestamp);
   //       
   //       return Ok(new
   //       {
   //          followerList
   //       });
   //    }
   //    catch (NotFoundRekomerException)
   //    {
   //       return NotFound();
   //    }
   // }
}