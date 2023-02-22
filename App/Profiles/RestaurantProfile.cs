using AutoMapper;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Profiles;

public class RestaurantProfile : Profile
{
   public RestaurantProfile()
   {
      CreateMap<Restaurant, RekomerRestaurantDetailResponseDto>();

      CreateMap<RatingResultDto, RekomerRestaurantDetailResponseDto>();
   }
}