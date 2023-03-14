using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFeedService : IRekomerFeedService
{
   private readonly IConfiguration _configuration;
   private readonly IRekomerReviewService _reviewService;
   private readonly IRekomerRestaurantService _restaurantService;
   private readonly IRekomerFoodService _foodService;
   
   public RekomerFeedService(IConfiguration configuration, IRekomerReviewService reviewService, IRekomerRestaurantService restaurantService, IRekomerFoodService foodService)
   {
      _configuration = configuration;
      _reviewService = reviewService;
      _restaurantService = restaurantService;
      _foodService = foodService;
   }

   public async Task<IEnumerable<RekomerFeedResponseDto>> GetFeedsAsync(string meId, RekomerGetFeedRequestDto getFeedRequest)
   {
      await using var dbContext = new RekomContext(_configuration);
      
      var userCurrentLocation = new Point(getFeedRequest.Location.Longitude, getFeedRequest.Location.Latitude);
      
      var restaurantIdList = await dbContext.Restaurants
         .OrderBy(res => res.Location.Distance(userCurrentLocation))
         .Skip((getFeedRequest.Page - 1) * getFeedRequest.Size)
         .Take(getFeedRequest.Size)
         .Select(res => res.Id)
         .ToListAsync();

      var feedList = new List<RekomerFeedResponseDto>();

      foreach (var resId in restaurantIdList)
      {
         var feed = new RekomerFeedResponseDto
         {
            StandardReview = (await _reviewService.GetReviewListByRestaurantAsync(meId, resId, 1, 1, null)).FirstOrDefault(),
            Restaurant = await _restaurantService.GetRestaurantCardAsync(resId),
            FoodList = await _foodService.GetFoodListInMenuAsync(resId, 1, 5)
         };
         
         feedList.Add(feed);
      }

      return feedList;
   }
}