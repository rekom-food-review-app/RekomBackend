using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Hubs.RekomerSideHubs;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerReviewService : IRekomerReviewService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;
   private readonly IHubContext<RekomerCommentHub> _commentHubContext;

   public RekomerReviewService(RekomContext context, IMapper mapper, IHubContext<RekomerCommentHub> commentHubContext)
   {
      _context = context;
      _mapper = mapper;
      _commentHubContext = commentHubContext;
   }

   public async Task<IEnumerable<RekomerReviewCardResponseDto>> GetRestaurantReviewListAsync(string meId, string restaurantId)
   {
      var restaurant = await _context.Restaurants
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Medias)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Rekomer)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Rating)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.ReviewReactions)
         .Include(res => res.Reviews!).ThenInclude(rev => rev.Comments)
         .AsNoTracking()
         .SingleOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant is null) throw new NotFoundRestaurantException();

      var reviewsResponseDto = restaurant.Reviews!.Select(rev =>
      {
         var revResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(rev);
         revResponse.Images = rev.Medias!.Select(med => med.MediaUrl);
         revResponse.AmountReply = rev.Comments!.Count();
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
         .Include(rev => rev.Comments)
         .Include(rev => rev.Medias)
         .Include(rev => rev.Rekomer)
         .Include(rev => rev.Rating)
         .Include(rev => rev.ReviewReactions)
         .SingleOrDefaultAsync(rev => rev.Id == reviewId);

      if (review is null) return null;

      var reviewResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(review);
      reviewResponse.Images = review.Medias!.Select(med => med.MediaUrl);
      reviewResponse.AmountReply = review.Comments!.Count();
      foreach (var reviewReaction in review.ReviewReactions!)
      {
         if (reviewReaction.RekomerId == meId) reviewResponse.MyReaction = reviewReaction.ReactionId;
         if (reviewReaction.ReactionId == "1") {reviewResponse.AmountAgree++; continue;}
         if (reviewReaction.ReactionId == "2") {reviewResponse.AmountDisagree++; continue;}
         if (reviewReaction.ReactionId == "3") reviewResponse.AmountUseful++;
      }
      
      return reviewResponse;
   }
   
   /// <param name="meId"></param>
   /// <param name="reviewId"></param>
   /// <param name="commentRequest"></param>
   /// <exception cref="InvalidAccessTokenException"></exception>
   /// <exception cref="NotFoundReviewException"></exception>
   public async Task CommentAsync(string meId, string reviewId, RekomerCommentRequestDto commentRequest)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var review = await _context.Reviews.SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();

      var comment = new Comment
      {
         RekomerId = meId,
         ReviewId = reviewId,
         Content = commentRequest.Content.Trim(),
         Rekomer = me
      };

      var commentResponse = _mapper.Map<RekomerCommentResponseDto>(comment);
      _ = _commentHubContext.Clients.All.SendAsync("ReceiveComment", commentResponse);
      
      _context.Comments.Add(comment);
      _ = _context.SaveChangesAsync();
   }
   
   /// <param name="reviewId"></param>
   /// <param name="page"></param>
   /// <param name="size"></param>
   /// <returns></returns>
   /// <exception cref="NotFoundReviewException"></exception>
   public async Task<IEnumerable<RekomerCommentResponseDto>> GetCommentListAsync(string reviewId, int page, int size)
   {
      var review = await _context.Reviews
         .Include(rev => rev.Comments!.Skip((page - 1) * size).Take(size).OrderByDescending(cmt => cmt.CreatedAt))
         .ThenInclude(cmt => cmt.Rekomer)
         .SingleOrDefaultAsync(rev => rev.Id == reviewId);

      if (review is null) throw new NotFoundReviewException();

      return review.Comments!.Select(cmt => _mapper.Map<RekomerCommentResponseDto>(cmt));
   }
}