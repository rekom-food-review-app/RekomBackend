using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFavouriteRestaurantService : IRekomerFavouriteRestaurantService
{
   private readonly RekomContext _context;

   public RekomerFavouriteRestaurantService(RekomContext context)
   {
      _context = context;
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
}