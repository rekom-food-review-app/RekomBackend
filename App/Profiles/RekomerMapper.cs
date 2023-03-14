using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class RekomerMapper : Profile
{
   public RekomerMapper()
   {
      CreateMap<Rekomer, RekomerProfileDetailResponseDto>();
      CreateMap<Rekomer, RekomerCardInfoResponseDto>();
      CreateMap<Rekomer, RekomerBasicProfile>();
   }
}