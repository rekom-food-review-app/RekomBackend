using AutoMapper;
using RekomBackend.App.Models.Dto.RekomerSideDtos;
using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Profiles;

public class ReviewMediaProfile : Profile
{
   public ReviewMediaProfile()
   {
      CreateMap<ReviewMedia, RekomerReviewMediaResponseDto>();
   }
}