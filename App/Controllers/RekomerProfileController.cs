using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("rekomers")]
public class RekomerProfileController : ControllerBase
{
   private readonly IRekomerProfileService _rekomerProfileService;

   public RekomerProfileController(IRekomerProfileService rekomerProfileService)
   {
      _rekomerProfileService = rekomerProfileService;
   }
   
   [HttpPut("me/profile"), Authorize(Roles = "Rekomer")]
   public async Task<IActionResult> CreateProfile([FromForm] PutRekomerProfileRequest putRequest)
   {
      try
      {
         await _rekomerProfileService.UpdateProfileAsync(putRequest);
      
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

   [HttpGet("me/profile"), Authorize(Roles = "Rekomer")]
   public async Task<IActionResult> GetMyProfile()
   {
      try
      {
         var myProfile = await _rekomerProfileService.GetMyProfileAsync();

         return Ok(new
         {
            myProfile
         });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }
}