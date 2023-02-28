using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/rekomers")]
[Authorize(Roles = "Rekomer")]
public class RekomerProfileController : ControllerBase
{
   private readonly IRekomerProfileService _profileService;
   private readonly IHttpContextAccessor _httpContextAccessor;

   public RekomerProfileController(IRekomerProfileService profileService, IHttpContextAccessor httpContextAccessor)
   {
      _profileService = profileService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpPut("me/profile")]
   public async Task<IActionResult> UpdateMyProfile([FromForm] RekomerUpdateProfileRequestDto updateRequest)
   {
      // try
      // {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      await _profileService.UpdateMyProfileAsync(meId, updateRequest);

      return Ok();
      // }
      // catch (InvalidAccessTokenException)
      // {
      //    return Unauthorized();
      // }
   }
   
   [HttpGet("me/profile")]
   public async Task<IActionResult> GetMyProfileDetail()
   {
      // try
      // {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      var rekomer = await _profileService.GetMyProfileDetailAsync(meId);

      return Ok(new
      {
         rekomer
      });
      // }
      // catch (InvalidAccessTokenException)
      // {
      //    return Unauthorized();
      // }
   }
   
   [HttpGet("{rekomerId}/profile")]
   public async Task<IActionResult> GetOtherProfileDetail(string rekomerId)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var rekomer = await _profileService.GetOtherProfileDetailAsync(meId, rekomerId);

         return Ok(new
         {
            rekomer
         });
      }
      catch (NotFoundRekomerException)
      {
         return NotFound();
      }
   }
}