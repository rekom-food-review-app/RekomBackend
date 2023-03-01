using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class FoodMapper : Profile
{
   public FoodMapper()
   {
      CreateMap<Food, RekomerFoodInMenuResponseDto>();
      CreateMap<Food, RekomerFoodDetailResponseDto>()
         .ForMember(
            dest => dest.PrimaryImage,
            opt => opt.MapFrom(src => src.ImageUrl)
         );
   }
}