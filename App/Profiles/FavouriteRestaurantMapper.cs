using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class FavouriteRestaurantMapper : Profile
{
   public FavouriteRestaurantMapper()
   {
      CreateMap<FavouriteRestaurant, RekomerFavRestaurantCardResponseDto>()
         .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.RestaurantId,
            opt => opt.MapFrom(src => src.RestaurantId)
         )
         .ForMember(
            dest => dest.RestaurantName,
            opt => opt.MapFrom(src => src.Restaurant!.Name)
         )
         .ForMember(
            dest => dest.RestaurantCoverImageUrl,
            opt => opt.MapFrom(src => src.Restaurant!.CoverImageUrl)
         )
         .ForMember(
            dest => dest.CreatedAt,
            opt => opt.MapFrom(src => src.CreatedAt)
         );
   }
}