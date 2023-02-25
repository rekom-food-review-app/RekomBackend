using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class RatingResultViewProfile : Profile
{
   public RatingResultViewProfile()
   {
      CreateMap<RatingResultView, RatingResultDto>()
         .ForMember(
            dest => dest.Amount,
            opt => opt.MapFrom(src => src.Amount.ToString())
         );
   }
}