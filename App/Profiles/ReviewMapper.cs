using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class ReviewMapper : Profile 
{
   public ReviewMapper()
   {
      CreateMap<Review, RekomerReviewCardResponseDto>()
         .ForMember(
            dest => dest.ReviewContent,
            opt => opt.MapFrom(src => src.Content)
         )
         .ForMember(
            dest => dest.ReviewAt,
            opt => opt.MapFrom(src => src.CreatedAt)
         )
         .ForMember(
         dest => dest.RekomerId,
         opt => opt.MapFrom(src => src.RekomerId)
         )
         .ForMember(
            dest => dest.RekomerFullName,
            opt => opt.MapFrom(src => src.Rekomer!.FullName)
         )
         .ForMember(
            dest => dest.RekomerAvatarUrl,
            opt => opt.MapFrom(src => src.Rekomer!.AvatarUrl)
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
            dest => dest.Rating,
            opt => opt.MapFrom(src => src.Rating!.Tag)
         );
   }
}