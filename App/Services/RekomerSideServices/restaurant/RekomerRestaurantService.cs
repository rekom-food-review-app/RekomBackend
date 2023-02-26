using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerRestaurantService : IRekomerRestaurantService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;

   public RekomerRestaurantService(RekomContext context, IMapper mapper)
   {
      _context = context;
      _mapper = mapper;
   }

   public async Task<RekomerRestaurantDetailDto?> GetRestaurantDetailAsync(string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) return null;

      var ratingResult = await _context.RatingResultViews.Distinct().SingleAsync(rat => rat.RestaurantId == restaurantId);

      var restaurantDto = _mapper.Map<Restaurant, RekomerRestaurantDetailDto>(restaurant);
      restaurantDto.RatingResult = ratingResult;
      
      return restaurantDto;
   }
}