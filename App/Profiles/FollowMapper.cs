using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class FollowMapper : Profile
{
   public FollowMapper()
   {
      CreateMap<Follow, RekomerFollowerResponseDto>()
         .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.RekomerId,
            opt => opt.MapFrom(src => src.Follower!.Id)
         )
         .ForMember(
            dest => dest.RekomerAvatarUrl,
            opt => opt.MapFrom(src => src.Follower!.AvatarUrl)
         )
         .ForMember(
            dest => dest.RekomerFullName,
            opt => opt.MapFrom(src => src.Follower!.FullName)
         )
         .ForMember(
            dest => dest.RekomerDescription,
            opt => opt.MapFrom(src => src.Follower!.Description)
         )
         .ForMember(
            dest => dest.CreatedAt,
            opt => opt.MapFrom(src => src.CreatedAt)
         );
      
      CreateMap<Follow, RekomerFollowingResponseDto>()
         .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.RekomerId,
            opt => opt.MapFrom(src => src.Following!.Id)
         )
         .ForMember(
            dest => dest.RekomerAvatarUrl,
            opt => opt.MapFrom(src => src.Following!.AvatarUrl)
         )
         .ForMember(
            dest => dest.RekomerFullName,
            opt => opt.MapFrom(src => src.Following!.FullName)
         )
         .ForMember(
            dest => dest.RekomerDescription,
            opt => opt.MapFrom(src => src.Following!.Description)
         )
         .ForMember(
            dest => dest.CreatedAt,
            opt => opt.MapFrom(src => src.CreatedAt)
         );
   }
}