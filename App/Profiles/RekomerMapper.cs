using AutoMapper;
using RekomBackend.App.Entities;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Dto.RekomerSideDtos;

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
            opt => opt.MapFrom(src => $"https://rekom-bucket.s3.ap-northeast-1.amazonaws.com/{src.AvatarUrl}")
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