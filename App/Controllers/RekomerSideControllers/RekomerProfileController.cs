using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
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
   public async Task<IActionResult> UpdateMyProfile([FromBody] RekomerUpdateProfileRequestDto updateRequest)
   {
      // try
      // {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      await _profileService.UpdateProfileAsync(meId, updateRequest);

      return Ok();
      // }
      // catch (InvalidAccessTokenException)
      // {
      //    return Unauthorized();
      // }
   }
}