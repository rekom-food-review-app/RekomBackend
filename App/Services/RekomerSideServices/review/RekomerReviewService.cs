using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Models.Dto;
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
         .Include(rev => rev.Medias)
         .Include(rev => rev.Rekomer)
         .Include(rev => rev.Restaurant)
         .Include(rev => rev.Rating)
         .Where(rev => rev.RestaurantId == restaurantId)
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