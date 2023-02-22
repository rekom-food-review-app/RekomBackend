using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto.RekomerSideDtos;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFoodService : IRekomerFoodService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;

   public RekomerFoodService(RekomContext context, IMapper mapper)
   {
      _context = context;
      _mapper = mapper;
   }

   public async Task<IEnumerable<RekomerFoodInMenuResponseDto>> GetFoodsInMenuAsync(string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .Include(res => res.Menu!.OrderBy(meu => meu.CreatedAt))
         // .ThenInclude(meu => meu.Images!.OrderBy(img => img.CreatedAt).Take(1))
         .AsNoTracking()
         .Where(res => res.Id == restaurantId)
         .SingleOrDefaultAsync();

      if (restaurant is null) { throw new NotFoundRestaurantException(); }
      
      return restaurant.Menu!.Select(fod => _mapper.Map<RekomerFoodInMenuResponseDto>(fod));
   }
}