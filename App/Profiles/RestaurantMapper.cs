using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Helpers;

namespace RekomBackend.App.Profiles;

public class RestaurantMapper : Profile
{
   public RestaurantMapper()
   {
      CreateMap<Restaurant, RekomerRestaurantDetailResponseDto>()
         .ForMember(
            dest => dest.Coordinates,
            opt => opt.MapFrom(src => new Coordinates()
            {
               Latitude = (float)src.Location.X,
               Longitude = (float)src.Location.Y
            })
         );
   }
}