using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services;
using RekomBackend.App.Services.rekomer_follow;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("rekomers")]
public class RekomerFollowController : ControllerBase
{
   private readonly IRekomerFollowService _rekomerFollowService;

   public RekomerFollowController(IRekomerFollowService rekomerFollowService)
   {
      _rekomerFollowService = rekomerFollowService;
   }

   [HttpGet("me/followers"), Authorize(Roles = "Rekomer")]
   public async Task<IActionResult> GetMyFollowers()
   {
      try
      {
         var myFollowers = await _rekomerFollowService.GetMyFollowers();

         return Ok(new
         {
            code = "S",
            message = "Here Is Your Followers",
            myFollowers
         });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
      throw new NotImplementedException();
   }

   [HttpPost("{id}/follow"), Authorize(Roles = "Rekomer")]
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

   [HttpDelete("{id}/unfollow"), Authorize(Roles = "Rekomer")]
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
   }
}