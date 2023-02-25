using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos;
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

   public async Task<RekomerRestaurantDetailResponseDto?> GetRestaurantProfileByIdAsync(string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .Include(res => res.Reviews!)
         .ThenInclude(rev => rev.Rating!)
         .Where(res => res.Id == restaurantId)
         .FirstOrDefaultAsync();

      if (restaurant is null) { return null; }
      
      var ratingResult = restaurant.CalculateRatingResult();
      
      var restaurantResponse = _mapper.Map<Restaurant, RekomerRestaurantDetailResponseDto>(restaurant);
      _mapper.Map(ratingResult, restaurantResponse);
      
      return restaurantResponse;
   }
}