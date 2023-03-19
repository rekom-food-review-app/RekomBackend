using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class ReviewReactionMapper : Profile
{
   public ReviewReactionMapper()
   {
      CreateMap<ReviewReaction, RekomerReactionResponseDto>()
         .ForMember(
            dest => dest.RekomerName,
            opt => opt.MapFrom(src => src.Rekomer!.FullName)
         )
         .ForMember(
            dest => dest.RekomerId,
            opt => opt.MapFrom(src => src.RekomerId)
         )
         .ForMember(
            dest => dest.RekomerAvatarUrl,
            opt => opt.MapFrom(src => src.Rekomer!.AvatarUrl)
         );
   }
}