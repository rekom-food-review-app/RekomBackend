﻿using AutoMapper;
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

   public async Task<IEnumerable<RekomerReviewCardResponseDto>> GetRestaurantReviewsAsync(string meId, string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Medias)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Rekomer)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Rating)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.ReviewReactions)
         .AsNoTracking()
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) throw new NotFoundRestaurantException();

      var reviewsResponseDto = restaurant.Reviews!.Select(rev =>
      {
         var revResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(rev);
         revResponse.Images = rev.Medias!.Select(med => med.MediaUrl);
         revResponse.AmountReply = 10;
         foreach (var reviewReaction in rev.ReviewReactions!)
         {
            if (reviewReaction.RekomerId == meId) revResponse.MyReaction = reviewReaction.ReactionId;
            if (reviewReaction.ReactionId == "1") {revResponse.AmountAgree++; continue;}
            if (reviewReaction.ReactionId == "2") {revResponse.AmountDisagree++; continue;}
            if (reviewReaction.ReactionId == "3") revResponse.AmountUseful++;
         }
         return revResponse;
      });
      
      return reviewsResponseDto;
   }

   public async Task<RekomerReviewCardResponseDto?> GetReviewDetailAsync(string meId, string reviewId)
   {
      var review = await _context.Reviews
         .Include(rev => rev.Restaurant)
         .Include(rev => rev.Medias)
         .Include(rev => rev.Rekomer)
         .Include(rev => rev.Rating)
         .Include(rev => rev.ReviewReactions)
         .SingleOrDefaultAsync(rev => rev.Id == reviewId);

      if (review is null) return null;

      var reviewResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(review);
      reviewResponse.Images = review.Medias!.Select(med => med.MediaUrl);
      reviewResponse.AmountReply = 10;
      foreach (var reviewReaction in review.ReviewReactions!)
      {
         if (reviewReaction.RekomerId == meId) reviewResponse.MyReaction = reviewReaction.ReactionId;
         if (reviewReaction.ReactionId == "1") {reviewResponse.AmountAgree++; continue;}
         if (reviewReaction.ReactionId == "2") {reviewResponse.AmountDisagree++; continue;}
         if (reviewReaction.ReactionId == "3") reviewResponse.AmountUseful++;
      }
      
      return reviewResponse;
   }
}