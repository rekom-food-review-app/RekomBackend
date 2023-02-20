using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("rekomers")]
public class RekomerController : ControllerBase
{
   private readonly IRekomerService _rekomerService;

   public RekomerController(IRekomerService rekomerService)
   {
      _rekomerService = rekomerService;
   }
   
   [HttpPost("me/profile"), Authorize(Roles = "Rekomer")]
   public async Task<IActionResult> CreateProfile([FromForm] CreateRekomerProfileRequest createRequest)
   {
      try
      {
         await _rekomerService.CreateProfileAsync(createRequest);
      
         return Ok(new
         {
            code = "CPS",
            message = "Create Profile Successfully."
         });
      }
      catch (InvalidAccessTokenException e)
      {
         return Unauthorized();
      }
      catch (RekomerProfileIsAlreadyCreatedException e)
      {
         return BadRequest(new
         {
            code = "PAC",
            message = "Your Profile Is Already Created."
         });
      }
   }

   [HttpPost("{id}/follow"), Authorize(Roles = "Rekomer")]
   public async Task<IActionResult> FollowOtherRekomer(string id)
   {
      try
      {
         await _rekomerService.FollowOtherRekomerAsync(id);

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
         await _rekomerService.UnfollowOtherRekomerAsync(id);

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