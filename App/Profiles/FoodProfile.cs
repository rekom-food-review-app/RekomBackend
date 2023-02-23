using AutoMapper;
using RekomBackend.App.Models.Dto.RekomerSideDtos;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Profiles;

public class FoodProfile : Profile
{
   public FoodProfile()
   {
      CreateMap<Food, RekomerFoodInMenuResponseDto>();

      CreateMap<Food, RekomerFoodDetailResponseDto>()
         .ForMember(
            dest => dest.FoodId,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.FoodName,
            opt => opt.MapFrom(src => src.Name)
         )
         .ForMember(
            dest => dest.FoodPrice,
            opt => opt.MapFrom(src => src.Price)
         )
         .ForMember(
            dest => dest.FoodPrimaryImage,
            opt => opt.MapFrom(src => src.ImageUrl)
         )
         .ForMember(
            dest => dest.RestaurantId,
            opt => opt.MapFrom(src => src.RestaurantId)
         )
         .ForMember(
            dest => dest.RestaurantName,
            opt => opt.MapFrom(src => src.Restaurant!.Name)
         );
   }
}