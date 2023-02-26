using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerReviewService : IRekomerReviewService
{
   private readonly RekomContext _context;

   public RekomerReviewService(RekomContext context)
   {
      _context = context;
   }

   public async Task<IEnumerable<Review>> GetRestaurantReviewsAsync(string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .Include(res => res.Reviews)
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) throw new NotFoundRestaurantException();

      return restaurant.Reviews!;
   }
}