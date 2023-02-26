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
      catch (NotFoundRekomerProfileException)
      {
         return NotFound();
      }
      catch (RekomerAlreadyFollowException)
      {
         return BadRequest(new
         {
            code = "AF",
            message = "You already follow this guy"
         });
      }
   }
}