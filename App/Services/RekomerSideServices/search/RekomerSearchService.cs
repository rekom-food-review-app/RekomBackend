using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerSearchService : IRekomerSearchService
{
   private readonly IMapper _mapper;
   private readonly IConfiguration _configuration;

   public RekomerSearchService(IMapper mapper, IConfiguration configuration)
   {
      _mapper = mapper;
      _configuration = configuration;
   }

   public async Task<IEnumerable<RekomerRestaurantCardResponseDto>> SearchForRestaurantAsync(RekomerSearchRequestDto searchRequest)
   {
      // var searchTerms = searchRequest.Keyword.Split(' ');
      //
      // var searchQuery = string.Join(" & ", searchTerms.Select(t => $"to_tsquery('{t}:*')"));
      // var searchVector = $"to_tsvector({searchQuery})";
      await using var dbContext = new RekomContext(_configuration);
      
      var restaurantList = await dbContext.Restaurants
         .Where(res => res.FullTextSearch.Matches(EF.Functions.ToTsQuery("english", string.Join(":* | ", searchRequest.Keyword.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)) + ":*" )))
         .Skip((searchRequest.Page - 1) * searchRequest.Size)
         .Take(searchRequest.Size)
         .ToListAsync();

      var restaurantResponseList = new List<RekomerRestaurantCardResponseDto>();

      foreach (var restaurant in restaurantList)
      {
         var restaurantResponse = _mapper.Map<RekomerRestaurantCardResponseDto>(restaurant);
         restaurantResponse.RatingAverage = dbContext.RatingResultViews.Distinct().Select(rat => rat.Average).FirstOrDefault();
         restaurantResponseList.Add(restaurantResponse);
      }
      
      await dbContext.DisposeAsync();

      return restaurantResponseList;
   }

   public async Task<IEnumerable<RekomerFoodInMenuResponseDto>> SearchForFoodAsync(RekomerSearchRequestDto searchRequest)
   {
      await using var dbContext = new RekomContext(_configuration);
      
      var foodList = await dbContext.Foods
         .Where(fod => fod.FullTextSearch.Matches(EF.Functions.ToTsQuery("english", string.Join(":* | ", searchRequest.Keyword.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)) + ":*" )))
         .Skip((searchRequest.Page - 1) * searchRequest.Size)
         .Take(searchRequest.Size)
         .ToListAsync();
      
      await dbContext.DisposeAsync();

      return foodList.Select(fod => _mapper.Map<RekomerFoodInMenuResponseDto>(fod));
   }

   public async Task<IEnumerable<RekomerCardInfoResponseDto>> SearchForRekomerAsync(string meId, RekomerSearchRequestDto searchRequest)
   {
      await using var dbContext = new RekomContext(_configuration); 
      
      var rekomerList = await dbContext.Rekomers
         .Where(rek => rek.FullTextSearch.Matches(EF.Functions.ToTsQuery("english", string.Join(":* | ", searchRequest.Keyword.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)) + ":*" )))
         .Skip((searchRequest.Page - 1) * searchRequest.Size)
         .Take(searchRequest.Size)
         .ToListAsync();
      
      await dbContext.DisposeAsync();
      
      return rekomerList.Select(rek => _mapper.Map<RekomerCardInfoResponseDto>(rek));
   }

   public async Task<RekomerSearchForAllResponse> SearchForAllAsync(string meId, RekomerSearchRequestDto searchRequest)
   {
      var searchRestaurantTask = SearchForRestaurantAsync(searchRequest);
      var searchFoodTask = SearchForFoodAsync(searchRequest);
      var searchRekomerTask = SearchForRekomerAsync(meId, searchRequest);
      
      await Task.WhenAll(
         searchRestaurantTask,
         searchFoodTask,
         searchRekomerTask
      );

      return new RekomerSearchForAllResponse
      {
         RestaurantList = searchRestaurantTask.Result,
         FoodList = searchFoodTask.Result,
         RekomerList = searchRekomerTask.Result
      };
   }
}