﻿using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFoodService
{
   public Task<IEnumerable<RekomerFoodInMenuResponseDto>> GetFoodListInMenuAsync(string restaurantId, int page, int size);
   
   public Task<RekomerFoodDetailResponseDto?> GetFoodDetailAsync(string foodId);
}