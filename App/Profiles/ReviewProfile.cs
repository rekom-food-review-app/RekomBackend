using AutoMapper;
using RekomBackend.App.Entities;
using RekomBackend.App.Models.Dto.RekomerSideDtos;

namespace RekomBackend.App.Profiles;

public class ReviewProfile : Profile
{
   public ReviewProfile()
   {
      CreateMap<Review, RekomerReviewCardResponseDto>()
         .ForMember(
            dest => dest.ReviewId,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.ReviewContent,
            opt => opt.MapFrom(src => src.Content)
         )
         .ForMember(
            dest => dest.ReviewTime,
            opt => opt.MapFrom(src => src.CreatedAt)
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
            dest => dest.RekomerId,
            opt => opt.MapFrom(src => src.RekomerId)
         )
         .ForMember(
            dest => dest.RekomerName,
            opt => opt.MapFrom(src => src.Rekomer!.FullName)
         )
         .ForMember(
            dest => dest.RekomerAvatarUrl,
            opt => opt.MapFrom(src => src.Rekomer!.AvatarUrl)
         );
   }
}