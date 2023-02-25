using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class AccountMapper : Profile
{
   public AccountMapper()
   {
      CreateMap<RekomerRegisterEmailRequestDto, Account>()
         .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => Guid.NewGuid().ToString())
         )
         .ForMember(
            dest => dest.Username,
            opt => opt.MapFrom(src => src.Username.ToLower().Trim())
         )
         .ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(src => src.Email.ToLower().Trim())
         );
   }
}