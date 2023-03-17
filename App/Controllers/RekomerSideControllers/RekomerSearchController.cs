using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[Route("rekomer-side/search")]
[ApiController]
[Authorize]
public class RekomerSearchController : ControllerBase
{
   private readonly IRekomerSearchService _searchService;
   private readonly IHttpContextAccessor _httpContextAccessor;

   public RekomerSearchController(IRekomerSearchService searchService, IHttpContextAccessor httpContextAccessor)
   {
      _searchService = searchService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpGet]
   public async Task<IActionResult> SearchForAll([FromQuery] RekomerSearchRequestDto searchRequest)
   {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      var result = await _searchService.SearchForAllAsync(meId, searchRequest);

      return Ok(new
      {
         result
      });
   }
   
   [HttpGet("restaurants")]
   public async Task<IActionResult> SearchForRestaurants([FromQuery]RekomerSearchRequestDto searchRequest)
   {
      var restaurantList = await _searchService.SearchForRestaurantAsync(searchRequest);

      return Ok(new
      {
         restaurantList
      });
   }

   [HttpGet("foods")]
   public async Task<IActionResult> SearchForFoods([FromQuery] RekomerSearchRequestDto searchRequest)
   {
      var foodList = await _searchService.SearchForFoodAsync(searchRequest);

      return Ok(new
      {
         foodList
      });
   }

   [HttpGet("rekomers")]
   public async Task<IActionResult> SearchForRekomers([FromQuery] RekomerSearchRequestDto searchRequest)
   {
      var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
      var rekomerList = await _searchService.SearchForRekomerAsync(meId, searchRequest);

      return Ok(new
      {
         rekomerList
      });
   }
}