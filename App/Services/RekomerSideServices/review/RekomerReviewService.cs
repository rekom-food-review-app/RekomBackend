using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerReviewService : IRekomerReviewService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;
   
   public RekomerReviewService(RekomContext context, IMapper mapper)
   {
      _context = context;
      _mapper = mapper;
   }

   public async Task<IEnumerable<RekomerReviewCardResponseDto>> GetRestaurantReviewsAsync(string restaurantId)
   {
      var restaurantQueryable = _context.Restaurants
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Medias)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Rekomer)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Rating)
         .Where(res => res.Id == restaurantId)
         .AsQueryable();

      var restaurant = await restaurantQueryable.SingleOrDefaultAsync();
      
      if (restaurant is null) throw new NotFoundRestaurantException();

      var reviewsResponseDto = restaurant.Reviews!.Select(rev =>
      {
         var revResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(rev);
         revResponse.Images = rev.Medias!.Select(med => med.MediaUrl);
         return revResponse;
      });
      
      return reviewsResponseDto;
   }

   public Task<RekomerReviewCardResponseDto> GetReviewDetail(string reviewId)
   {
      throw new NotImplementedException();
   }
}