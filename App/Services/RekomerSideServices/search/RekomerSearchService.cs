using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerSearchService : IRekomerSearchService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;

   public RekomerSearchService(RekomContext context, IMapper mapper)
   {
      _context = context;
      _mapper = mapper;
   }

   public async Task<IEnumerable<RekomerRestaurantCardResponseDto>> SearchForRestaurant(RekomerSearchForRestaurantRequestDto searchRequest)
   {
      // var searchTerms = searchRequest.Keyword.Split(' ');
      //
      // var searchQuery = string.Join(" & ", searchTerms.Select(t => $"to_tsquery('{t}:*')"));
      // var searchVector = $"to_tsvector({searchQuery})";
      
      var restaurantList = await _context.Restaurants
         .Where(res => res.FullTextSearch.Matches(searchRequest.Keyword))
         .Skip((searchRequest.Page - 1) * searchRequest.Size)
         .Take(searchRequest.Size)
         .ToListAsync();
      
      var restaurantListResponse = restaurantList.Select(res =>
      {
         var restaurantResponse = _mapper.Map<RekomerRestaurantCardResponseDto>(res);
         restaurantResponse.RatingAverage = _context.RatingResultViews.Distinct().Select(rat => rat.Average).FirstOrDefault();
         return restaurantResponse;
      });

      return restaurantListResponse;
   }
}