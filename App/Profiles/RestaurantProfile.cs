using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class RestaurantProfile : Profile
{
   public RestaurantProfile()
   {
      CreateMap<Restaurant, RekomerRestaurantDetailResponseDto>();

      CreateMap<RatingResultDto, RekomerRestaurantDetailResponseDto>();
   }
}