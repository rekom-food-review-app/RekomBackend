using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Entities;
using RekomBackend.App.Services.RekomerSideServices;
using RekomBackend.Database;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/feeds")]
public class RekomerFeedController : ControllerBase
{
   private readonly IRekomerFeedService _feedService;
   private readonly RekomContext _context;

   public RekomerFeedController(IRekomerFeedService feedService, RekomContext context)
   {
      _feedService = feedService;
      _context = context;
   }

   [HttpGet]
   public async Task<IActionResult> GetFeeds(RekomerGetFeedsRequestDto getRequest)
   {
      return Ok();
   }

   [HttpGet("test")]
   public async Task<IActionResult> GetTest()
   {
      var res = await _context.Restaurants.SingleOrDefaultAsync(res => res.Id == "0b4a446e-c238-4d5f-aa6e-527265bae629");
      // res.Location = new Point(21.04669195228524, 105.85447302616494);
      // await _context.SaveChangesAsync();
      return Ok(new
      {
         x = res!.Location.X,
         y = res.Location.Y,
      });
   }
}