using AutoMapper;
using RekomBackend.App.Models.Dto.go;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Profiles;

public class RekomerMapper : Profile
{
   public RekomerMapper()
   {
      CreateMap<Rekomer, RekomerProfileResponse>()
         .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id)
         )
         .ForMember(
            dest => dest.Username,
            opt => opt.MapFrom(src => src.Account!.Username)
         )
         .ForMember(
            dest => dest.AvatarUrl,
            opt => opt.MapFrom(src => $"https://s3.console.aws.amazon.com/s3/object/rekom-bucket?region=ap-northeast-1&prefix={src.AvatarUrl}")
         )
         .ForMember(
            dest => dest.FullName,
            opt => opt.MapFrom(src => src.FullName)
         )
         .ForMember(
            dest => dest.Description,
            opt => opt.MapFrom(src => src.Description)
         );
   }
}