using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/rekomers")]
[Authorize(Roles = "Rekomer")]
public class RekomerProfileController : ControllerBase
{
   private readonly IRekomerProfileService _rekomerProfileService;

   public RekomerProfileController(IRekomerProfileService rekomerProfileService)
   {
      _rekomerProfileService = rekomerProfileService;
   }
   
   [HttpPut("me/profile")]
   public async Task<IActionResult> UpdateProfile([FromForm] PutRekomerProfileRequest putRequest)
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

   [HttpGet("me/profile")]
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

   [HttpGet("{id}/profile")]
   public async Task<IActionResult> GetOtherProfile(string id)
   {
      try
      {
         var otherProfile = await _rekomerProfileService.GetOtherProfile(id);

         return Ok(new
         {
            otherProfile
         });
      }
      catch (NotFoundRekomerProfileException e)
      {
         return NotFound();
      }
   }
}