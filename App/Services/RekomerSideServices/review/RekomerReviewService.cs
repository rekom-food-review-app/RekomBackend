using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Models.Dto.RekomerSideDtos;
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

   public async Task<IEnumerable<RekomerReviewCardResponseDto>> GetReviewCardsByRestaurantAsync(string restaurantId, int? page = null, int? limit = null)
   {
      var reviews = await _context.Reviews
         .AsNoTracking()
         .Include(rev => rev.Medias!.OrderBy(med => med.CreatedAt))
         .Include(rev => rev.Rekomer)
         .Include(rev => rev.Restaurant)
         .Include(rev => rev.Rating)
         .Where(rev => rev.RestaurantId == restaurantId)
         .OrderBy(rev => rev.CreatedAt)
         // .ProjectTo<RekomerReviewCardResponseDto>(_mapper.Con)
         .ToListAsync();

      return reviews.Select(rev =>
      {
         var revResponse = _mapper.Map<RekomerReviewCardResponseDto>(rev);
         revResponse.ReviewMedias = rev.Medias!.Select(med => _mapper.Map<RekomerReviewMediaResponseDto>(med));
         return revResponse;
      });
   }
}