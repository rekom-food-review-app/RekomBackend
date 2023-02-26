using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class RestaurantMapper : Profile
{
   public RestaurantMapper()
   {
      CreateMap<Restaurant, RekomerRestaurantDetailDto>();
   }
}