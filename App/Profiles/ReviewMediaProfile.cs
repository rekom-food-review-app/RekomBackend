using AutoMapper;
using RekomBackend.App.Entities;
using RekomBackend.App.Models.Dto.RekomerSideDtos;

namespace RekomBackend.App.Profiles;

public class ReviewMediaProfile : Profile
{
   public ReviewMediaProfile()
   {
      CreateMap<ReviewMedia, RekomerReviewMediaResponseDto>();
   }
}