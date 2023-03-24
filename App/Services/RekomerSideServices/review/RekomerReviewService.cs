using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.App.Hubs.RekomerSideHubs;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerReviewService : IRekomerReviewService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;
   private readonly IHubContext<RekomerCommentHub> _commentHubContext;
   private readonly IS3Helper _s3Helper;
   private readonly IRekomerCreatReviewRateLimit _creatReviewRateLimit;

   public RekomerReviewService(RekomContext context, IMapper mapper, IHubContext<RekomerCommentHub> commentHubContext, IS3Helper s3Helper, IRekomerCreatReviewRateLimit creatReviewRateLimit)
   {
      _context = context;
      _mapper = mapper;
      _commentHubContext = commentHubContext;
      _s3Helper = s3Helper;
      _creatReviewRateLimit = creatReviewRateLimit;
   }

   public async Task<IEnumerable<RekomerReviewCardResponseDto>> GetReviewListByRestaurantAsync(
      string meId, string restaurantId, int page, int size, DateTime? lastTimestamp = null)
   {
      var restaurant = await _context.Restaurants.SingleOrDefaultAsync(res => res.Id == restaurantId);
      if (restaurant is null) throw new NotFoundRestaurantException();
      
      var reviewListQuery = _context.Reviews
         .Where(rev => rev.RestaurantId == restaurantId)
         .OrderByDescending(rev => rev.CreatedAt)
         .Skip((page - 1) * size)
         .AsQueryable();
      
      if (lastTimestamp.HasValue) reviewListQuery = reviewListQuery.Where(rev => rev.CreatedAt < lastTimestamp.Value);

      var reviewList = await reviewListQuery
         .Take(size)
         .Include(rev => rev.Restaurant)
         .Include(rev => rev.Comments)
         .Include(rev => rev.Medias)
         .Include(rev => rev.Rekomer)
         .Include(rev => rev.Rating)
         .Include(rev => rev.ReviewReactions)
         .ToListAsync();
      
      var reviewListDto = reviewList.Select(rev =>
      {
         var reviewResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(rev);
         reviewResponse.Images = rev.Medias!.Select(med => med.MediaUrl);
         reviewResponse.AmountReply = rev.Comments!.Count();
         foreach (var reviewReaction in rev.ReviewReactions!)
         {
            if (reviewReaction.RekomerId == meId) reviewResponse.MyReactionId = reviewReaction.ReactionId;
         }
         return reviewResponse;
      });

      return reviewListDto;
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
         if (reviewReaction.RekomerId == meId) reviewResponse.MyReactionId = reviewReaction.ReactionId;
      }
      
      return reviewResponse;
   }
   
   public async Task CommentAsync(string meId, string reviewId, RekomerCommentRequestDto commentRequest)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var review = await _context.Reviews.SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();

      review.AmountReply = 1 + (uint) await _context.Comments.Where(cmt => cmt.ReviewId == reviewId).CountAsync();
      
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
      await _context.SaveChangesAsync();
   }
   
   public async Task<IEnumerable<RekomerCommentResponseDto>> GetCommentListAsync(string reviewId, int page, int size, DateTime? lastTimestamp = null)
   {
      var review = await _context.Reviews.SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();

      var commentListQuery = _context.Comments
         .Where(cmt => cmt.ReviewId == reviewId)
         .Include(cmt => cmt.Rekomer)
         .OrderByDescending(cmt => cmt.CreatedAt)
         .Skip((page - 1) * size)
         .AsQueryable();
      
      if (lastTimestamp.HasValue) commentListQuery = commentListQuery.Where(cmt => cmt.CreatedAt < lastTimestamp.Value);
      
      return commentListQuery
         .Take(size)
         .Select(cmt => _mapper.Map<RekomerCommentResponseDto>(cmt));
   }

   public async Task<RekomerReviewCardResponseDto> CreateReviewAsync(string meId, string restaurantId, RekomerCreateReviewRequestDto reviewRequest)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var restaurant = await _context.Restaurants.SingleOrDefaultAsync(res => res.Id == restaurantId);
      if (restaurant is null) throw new NotFoundRestaurantException();

      var canReview = await _creatReviewRateLimit.IsAllowedAsync(meId);
      if (!canReview) throw new TooManyRequestException();
      
      var mediaUrlList = await _s3Helper.UploadManyFileAsync(reviewRequest.Images);

      var newReview = new Review
      {
         RekomerId = meId,
         Rekomer = me,
         Restaurant = restaurant,
         RatingId = reviewRequest.Rating,
         RestaurantId = restaurantId,
         Content = reviewRequest.Content,
         Medias = mediaUrlList.Select(url => new ReviewMedia
         {
            MediaUrl = url,
            Type = "image"
         }).ToList()
      };
      _context.Reviews.Add(newReview);
      await _creatReviewRateLimit.IncreaseRequestTimeByOne(meId);
      await _context.SaveChangesAsync();

      return _mapper.Map<RekomerReviewCardResponseDto>(newReview);
   }

   public async Task<IEnumerable<RekomerReactionResponseDto>> GetReactionListAsync(string reviewId, string reactionId, int page, int size, DateTime? lastTimestamp = null)
   {
      var review = await _context.Reviews.AsNoTracking().SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();
   
      var reactionListQuery = _context.ReviewReactions
         .Where(rea => rea.ReviewId == reviewId && rea.ReactionId == reactionId)
         .OrderByDescending(cmt => cmt.CreatedAt)
         .Skip((page - 1) * size)
         .AsQueryable();
      
      if (lastTimestamp.HasValue) reactionListQuery = reactionListQuery.Where(rea => rea.CreatedAt < lastTimestamp);

      var reactionList = await reactionListQuery
         .Take(size)
         .Include(rea => rea.Rekomer)
         .ToListAsync();

      return reactionList.Select(rea => _mapper.Map<RekomerReactionResponseDto>(rea));
   }

   public async Task ReactToReviewAsync(string meId, string reviewId, string reactionId)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var review = await _context.Reviews.SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();

      var oldReaction = await _context.ReviewReactions.SingleOrDefaultAsync(rre => rre.ReviewId == reviewId && rre.RekomerId == meId);
      if (oldReaction != null) _context.ReviewReactions.Remove(oldReaction);
      
      _context.ReviewReactions.Add(new ReviewReaction
      {
         ReactionId = reactionId,
         ReviewId = reviewId,
         RekomerId = meId
      });
      await _context.SaveChangesAsync();
      await UpdateReviewReactionAsync(reviewId);
   }

   private async Task<uint> GetReviewReactionAmount(string reviewId, string reactionId)
   {
      return (uint)await _context.ReviewReactions
         .Where(rre => rre.ReviewId == reviewId && rre.ReactionId == reactionId)
         .CountAsync();
   }

   private async Task UpdateReviewReactionAsync(string reviewId)
   {
      var review = await _context.Reviews.SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();
      
      review.AmountAgree = await GetReviewReactionAmount(review.Id, "1"); 
      review.AmountDisagree = await GetReviewReactionAmount(review.Id, "2");
      review.AmountUseful = await GetReviewReactionAmount(review.Id, "3");

      await _context.SaveChangesAsync();
   }

   public async Task UnReactToReviewAsync(string meId, string reviewId, string reactionId)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var review = await _context.Reviews.SingleOrDefaultAsync(rev => rev.Id == reviewId);
      if (review is null) throw new NotFoundReviewException();

      var oldReaction = await _context.ReviewReactions.SingleOrDefaultAsync(rre => rre.ReviewId == reviewId && rre.RekomerId == meId);

      if (oldReaction is null) throw new Exception();
      
      _context.ReviewReactions.Remove(oldReaction);
      
      await _context.SaveChangesAsync();
      await UpdateReviewReactionAsync(reviewId);
   }

   public async Task<IEnumerable<RekomerReviewCardResponseDto>> GetReviewListByRekomerAsync(
      string meId, string rekomerId, int page, int size, DateTime? lastTimestamp = null)
   {
      var rekomer = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == rekomerId);
      if (rekomer is null) throw new NotFoundRekomerException();
      
      var reviewListQuery = _context.Reviews
         .Where(rev => rev.RekomerId == rekomerId)
         .OrderByDescending(rev => rev.CreatedAt)
         .Skip((page - 1) * size)
         .AsQueryable();
      
      if (lastTimestamp.HasValue) reviewListQuery = reviewListQuery.Where(rev => rev.CreatedAt < lastTimestamp.Value);

      var reviewList = await reviewListQuery
         .Take(size)
         .Include(rev => rev.Restaurant)
         .Include(rev => rev.Comments)
         .Include(rev => rev.Medias)
         .Include(rev => rev.Rekomer)
         .Include(rev => rev.Rating)
         .Include(rev => rev.ReviewReactions)
         .ToListAsync();
      
      var reviewListDto = reviewList.Select(rev =>
      {
         var reviewResponse = _mapper.Map<Review, RekomerReviewCardResponseDto>(rev);
         reviewResponse.Images = rev.Medias!.Select(med => med.MediaUrl);
         reviewResponse.AmountReply = rev.Comments!.Count();
         foreach (var reviewReaction in rev.ReviewReactions!)
         {
            if (reviewReaction.RekomerId == meId) reviewResponse.MyReactionId = reviewReaction.ReactionId;
         }
         return reviewResponse;
      });

      return reviewListDto;
   }
}