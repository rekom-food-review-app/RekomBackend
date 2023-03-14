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
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.Content,
            opt => opt.MapFrom(src => src.Content)
         )
         .ForMember(
            dest => dest.CreatedAt,
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
            dest => dest.RatingId,
            opt => opt.MapFrom(src => src.RatingId)
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
            dest => dest.AmountAgree,
            opt => opt.MapFrom(src => src.AmountAgree)
         )
         .ForMember(
            dest => dest.AmountUseful,
            opt => opt.MapFrom(src => src.AmountUseful)
         )
         .ForMember(
            dest => dest.AmountDisagree,
            opt => opt.MapFrom(src => src.AmountDisagree)
         )
         .ForMember(
            dest => dest.AmountReply,
            opt => opt.MapFrom(src => src.AmountReply)
         );
   }
}