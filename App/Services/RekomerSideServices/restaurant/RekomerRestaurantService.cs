using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
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

   public async Task<RekomerRestaurantDetailResponseDto?> GetRestaurantDetailAsync(string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) return null;

      var ratingResult = await _context.RatingResultViews.Distinct().SingleAsync(rat => rat.RestaurantId == restaurantId);
      ratingResult.Average = (float)Math.Round(ratingResult.Average, 1);
      
      var restaurantDto = _mapper.Map<Restaurant, RekomerRestaurantDetailResponseDto>(restaurant);
      restaurantDto.RatingResult = ratingResult;
      
      return restaurantDto;
   }

   public async Task<IEnumerable<string>> GetRestaurantGalleryAsync(string restaurantId, int page, int size)
   {
      var restaurant = await _context.Restaurants
         .Include(res => res.Reviews!.Skip((page - 1) * size).Take(size))
         .ThenInclude(rev => rev.Medias)
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) throw new NotFoundRestaurantException();

      var gallery = new List<string>();

      foreach (var review in restaurant.Reviews!)
      {
         gallery.AddRange(review.Medias!.Select(med => med.MediaUrl));
      }

      return gallery;
   }
}