using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[Route("rekomer-side/search")]
[ApiController]
public class RekomerSearchController : ControllerBase
{
   private readonly IRekomerSearchService _searchService;

   public RekomerSearchController(IRekomerSearchService searchService)
   {
      _searchService = searchService;
   }

   [HttpGet]
   public async Task<IActionResult> SearchForRestaurants([FromQuery]RekomerSearchRequestDto searchRequest)
   {
      var restaurantList = await _searchService.SearchForRestaurant(searchRequest);

      return Ok(new
      {
         restaurantList
      });
   }
}