﻿using RekomBackend.App.Models.Dto.RekomerSideDtos;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFoodService
{
   public Task<IEnumerable<RekomerFoodInMenuResponseDto>> GetFoodsInMenuAsync(string restaurantId);
}