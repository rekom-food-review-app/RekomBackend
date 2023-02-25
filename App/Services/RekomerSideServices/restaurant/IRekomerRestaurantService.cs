﻿using RekomBackend.App.Dto.RekomerSideDtos;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRestaurantService
{
   public Task<RekomerRestaurantDetailResponseDto?> GetRestaurantProfileByIdAsync(string restaurantId);
}
