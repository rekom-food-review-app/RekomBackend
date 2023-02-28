using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class CommentMapper : Profile
{
   public CommentMapper()
   {
      CreateMap<Comment, RekomerCommentResponseDto>()
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
         )
         .ForMember(
            dest => dest.Content,
            opt => opt.MapFrom(src => src.Content)
         )
         .ForMember(
            dest => dest.CreatedAt,
            opt => opt.MapFrom(src => src.CreatedAt)
         )
         ;
   }
}