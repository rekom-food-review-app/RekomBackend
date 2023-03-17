using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[Route("rekomer-side/feeds")]
[ApiController]
[Authorize(Roles = "Rekomer")]
public class RekomerFeedController : ControllerBase
{
   private readonly IRekomerFeedService _feedService;
   private readonly IHttpContextAccessor _httpContextAccessor;
   
   public RekomerFeedController(IRekomerFeedService feedService, IHttpContextAccessor httpContextAccessor)
   {
      _feedService = feedService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpGet]
   public async Task<IActionResult> GetFeeds([FromQuery] RekomerGetFeedRequestDto getFeedRequest)
   {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      var feedList = await _feedService.GetFeedsAsync(meId, getFeedRequest);
      
      return Ok(new
      {
         feedList
      });
   }
}