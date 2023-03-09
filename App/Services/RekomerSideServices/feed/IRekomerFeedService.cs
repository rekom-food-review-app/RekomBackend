﻿using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerFeedService
{
   public Task<IEnumerable<RekomerRestaurantDetailResponseDto>> GetFeeds(RekomerGetFeedsRequestDto getRequest);
}