using AutoMapper;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Profiles;

public class ReviewMediaProfile : Profile
{
   public ReviewMediaProfile()
   {
      CreateMap<ReviewMedia, RekomerReviewMediaResponseDto>();
   }
}