using AutoMapper;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Profiles;

public class AccountMapper : Profile
{
   public AccountMapper()
   {
      CreateMap<RegisterWithEmailRequest, Account>()
      .ForMember(
         dest => dest.Id,
         opt => opt.MapFrom(src => Guid.NewGuid().ToString())
      )
      .ForMember(
         dest => dest.Username,
         opt => opt.MapFrom(src => src.Username)
      )
      .ForMember(
         dest => dest.Email,
         opt => opt.MapFrom(src => src.Email)
      )
      .ForMember(
         dest => dest.Role,
         opt => opt.MapFrom(src => (Role)Enum.Parse(typeof(Role), src.Role))
      );
   }
}