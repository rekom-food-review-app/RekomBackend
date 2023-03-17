using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFavouriteRestaurantService : IRekomerFavouriteRestaurantService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;

   public RekomerFavouriteRestaurantService(RekomContext context, IMapper mapper)
   {
      _context = context;
      _mapper = mapper;
   }

   public async Task AddAsync(string meId, string restaurantId)
   {
      var me = await _context.Rekomers.FindAsync(meId);
      if (me is null) throw new NotFoundRekomerException();
      
      var restaurant = await _context.Restaurants.FindAsync(restaurantId);
      if (restaurant is null) throw new NotFoundRestaurantException();
      
      var favouriteRestaurant = await _context.FavouriteRestaurants
         .SingleOrDefaultAsync(fav => fav.RekomerId == meId && fav.RestaurantId == restaurantId);
      if (favouriteRestaurant is not null) throw new RestaurantAlreadyInFavouriteListException();
      
      _context.FavouriteRestaurants.Add(new FavouriteRestaurant
      {
         RekomerId = meId,
         RestaurantId = restaurantId
      });
      await _context.SaveChangesAsync();
   }

   public async Task DeleteAsync(string meId, string restaurantId)
   {
      var me = await _context.Rekomers.FindAsync(meId);
      if (me is null) throw new NotFoundRekomerException();
      
      var favouriteRestaurant = await _context.FavouriteRestaurants
         .SingleOrDefaultAsync(fav => fav.RekomerId == meId && fav.RestaurantId == restaurantId);
      if (favouriteRestaurant is null) throw new NotFoundRestaurantInFavouriteListException();
      
      _context.FavouriteRestaurants.Remove(favouriteRestaurant);
      await _context.SaveChangesAsync();
   }

   public async Task<IEnumerable<RekomerFavRestaurantCardResponseDto>> GetMyFavouriteList(string meId, int page, int size, DateTime? lastTimestamp = null)
   {
      var me = await _context.Rekomers.FindAsync(meId);
      if (me is null) throw new InvalidAccessTokenException();

      var favListQuery = _context.FavouriteRestaurants
         .Where(fav => fav.RekomerId == meId)
         .AsQueryable();
      
      if (lastTimestamp.HasValue) favListQuery = favListQuery.Where(rea => rea.CreatedAt < lastTimestamp);

      var favList = await favListQuery
         .OrderByDescending(cmt => cmt.CreatedAt)
         .Skip((page - 1) * size)
         .Take(size)
         .Include(fav => fav.Restaurant)
         .ToListAsync();

      var favListResponse = favList
         .OrderByDescending(fav => fav.CreatedAt)
         .Select(fav => _mapper.Map<RekomerFavRestaurantCardResponseDto>(fav))
         .ToList();
      
      foreach (var fav in favListResponse)
      {
         var ratingResult = await _context.RatingResultViews
            .Distinct()
            .SingleOrDefaultAsync(rat => rat.RestaurantId == fav.RestaurantId);

         if (ratingResult != null) fav.RestaurantRatingAverage = (float)Math.Round(ratingResult.Average, 1);
      }

      return favListResponse;
   }
}