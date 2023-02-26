﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Exceptions;
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
         .Include(res => res.Menu)
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) throw new NotFoundRestaurantException();

      return restaurant.Menu!.Select(fod => _mapper.Map<RekomerFoodInMenuResponseDto>(fod));
   }
}