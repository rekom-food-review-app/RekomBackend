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
   private readonly IRekomerCreatReviewRateLimit _creatReviewRateLimit;

   public RekomerRestaurantService(RekomContext context, IMapper mapper, IRekomerCreatReviewRateLimit creatReviewRateLimit)
   {
      _context = context;
      _mapper = mapper;
      _creatReviewRateLimit = creatReviewRateLimit;
   }

   public async Task<RekomerRestaurantDetailResponseDto?> GetRestaurantDetailAsync(string meId, string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) return null;

      var ratingResult = await _context.RatingResultViews.Distinct().FirstOrDefaultAsync(rat => rat.RestaurantId == restaurantId) 
                         ??
                         new RatingResultView();

      var restaurantDto = _mapper.Map<Restaurant, RekomerRestaurantDetailResponseDto>(restaurant);
      restaurantDto.RatingResult = ratingResult;
      restaurantDto.IsMyFav = await _context.FavouriteRestaurants
            .FirstOrDefaultAsync(fav => fav.RekomerId == meId && fav.RestaurantId == restaurantId) 
         is not null;
      restaurantDto.CanReview = await _creatReviewRateLimit.IsAllowedAsync(meId);
      
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

   public async Task<RekomerRestaurantCardResponseDto> GetRestaurantCardAsync(string restaurantId)
   {
      var restaurant = await _context.Restaurants.SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) throw new NotFoundRestaurantException();
      
      var ratingResult = await _context.RatingResultViews.Distinct()
         .SingleOrDefaultAsync(rat => rat.RestaurantId == restaurantId);

      var restaurantResponse = _mapper.Map<RekomerRestaurantCardResponseDto>(restaurant);

      if (ratingResult is not null)
      {
         restaurantResponse.RatingAverage = ratingResult.Average;
      }
      
      return restaurantResponse;
   }
}