using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[Route("rekomer-side/feeds")]
[ApiController]
public class RekomerFeedController : ControllerBase
{
   [HttpGet]
   public async Task<IActionResult> GetFeeds([FromQuery] RekomerGetFeedRequestDto getFeedRequest)
   {
      return Ok();
   }
}