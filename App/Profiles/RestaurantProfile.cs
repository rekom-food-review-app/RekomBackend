using AutoMapper;
using RekomBackend.App.Entities;
using RekomBackend.App.Models.Dto.RekomerSideDtos;

namespace RekomBackend.App.Profiles;

public class RestaurantProfile : Profile
{
   public RestaurantProfile()
   {
      CreateMap<Restaurant, RekomerRestaurantDetailResponseDto>();

      CreateMap<RatingResultDto, RekomerRestaurantDetailResponseDto>();
   }
}