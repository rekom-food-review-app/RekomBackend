using System.ComponentModel.DataAnnotations;
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
   private readonly IRekomerFollowService _rekomerFollowService;

   public RekomerFollowController(IRekomerFollowService rekomerFollowService)
   {
      _rekomerFollowService = rekomerFollowService;
   }

   [HttpGet("me/followers")]
   public async Task<IActionResult> GetMyFollowers([FromQuery, RegularExpression("^[1-9]\\d*$")] int? page, [FromQuery, RegularExpression("^[1-9]\\d*$")] int? limit)
   {
      try
      {
         var myFollowers = await _rekomerFollowService.GetMyFollowersAsync(page, limit);

         return Ok(new
         {
            code = "S",
            message = "Here Are Followers",
            myFollowers
         });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }
   
   [HttpGet("{id}/followers")]
   public async Task<IActionResult> GetOtherFollowers(string id, [FromQuery, RegularExpression("^[1-9]\\d*$")] int? page, [FromQuery, RegularExpression("^[1-9]\\d*$")] int? limit)
   {
      try
      {
         var myFollowers = await _rekomerFollowService.GetOtherFollowersAsync(id, page, limit);

         return Ok(new
         {
            code = "S",
            message = "Here Are Followers",
            myFollowers
         });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }
   
   [HttpGet("me/followings")]
   public async Task<IActionResult> GetMyFollowings([FromQuery, RegularExpression("^[1-9]\\d*$")] int? page, [FromQuery, RegularExpression("^[1-9]\\d*$")] int? limit)
   {
      try
      {
         var myFollowers = await _rekomerFollowService.GetMyFollowingsAsync(page, limit);

         return Ok(new
         {
            code = "S",
            message = "Here Are Followings",
            myFollowers
         });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }
   
   [HttpGet("{id}/followings")]
   public async Task<IActionResult> GetOtherFollowings(string id, [FromQuery, RegularExpression("^[1-9]\\d*$")] int? page, [FromQuery, RegularExpression("^[1-9]\\d*$")] int? limit)
   {
      try
      {
         var myFollowers = await _rekomerFollowService.GetOtherFollowingsAsync(id, page, limit);

         return Ok(new
         {
            code = "S",
            message = "Here Are Followings",
            myFollowers
         });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }

   [HttpPost("{id}/follow")]
   public async Task<IActionResult> FollowOtherRekomer(string id)
   {
      try
      {
         await _rekomerFollowService.FollowOtherRekomerAsync(id);

         return Ok(new
         {
            code = "FS",
            message = "Follow Rekomer Successfully."
         });
      }
      catch (YourProfileIsNotCreatedYetException e)
      {
         return BadRequest(new
         {
            code = "YPCY",
            message = "Please Create Your Profile First."
         });
      }
      catch (NotFoundRekomerProfileException e)
      {
         return NotFound(new
         {
            code = "NFR",
            message = "This Rekomer Is Not Existed."
         });
      }
      catch (FollowYourSelfException e)
      {
         return BadRequest(new
         {
            code = "FYSE",
            message = "Don't Try Follow Your Self."
         });
      }
   }

   [HttpDelete("{id}/unfollow")]
   public async Task<IActionResult> UnfollowOtherRekomer(string id)
   {
      try
      {
         await _rekomerFollowService.UnfollowOtherRekomerAsync(id);

         return Ok(new
         {
            code = "UFS",
            message = "Unfollow Rekomer Successfully."
         });
      }
      catch (YourProfileIsNotCreatedYetException e)
      {
         return BadRequest(new
         {
            code = "YPCY",
            message = "Please Create Your Profile First."
         });
      }
      catch (NotFoundRekomerProfileException e)
      {
         return NotFound(new
         {
            code = "NFR",
            message = "This Rekomer Is Not Existed."
         });
      }
      catch (YouDidNotFollowThisRekomerYetException e)
      {
         return BadRequest(new
         {
            code = "UFF",
            message = "You Did Not Follow This Rekomer Yet."
         });
      }
   }
}