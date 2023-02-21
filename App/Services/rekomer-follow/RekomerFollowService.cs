using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class RekomerFollowService : IRekomerFollowService
{
   private readonly RekomContext _context;
   private readonly ITokenService _tokenService;
   
   public RekomerFollowService(RekomContext context, ITokenService tokenService)
   {
      _context = context;
      _tokenService = tokenService;
   }

   public async Task FollowOtherRekomerAsync(string rekomerId)
   {
      var accountPromise = _tokenService.GetRekomerAccountByReadingAccessToken();
      var followingPromise = _context.Rekomers.FindAsync(rekomerId);
      
      var follower = (await accountPromise).Rekomer;
      var following = await followingPromise;
      
      if (follower is null) { throw new YourProfileIsNotCreatedYetException(); }
      if (following is null) { throw new NotFoundRekomerProfileException();}
      if (follower.Id == following.Id) { throw new FollowYourSelfException(); }
      
      var follow = new Follow
      {
         Id = Guid.NewGuid().ToString(),
         Follower = follower,
         Following = following
      };

      _context.Follows.Add(follow);
      _ = _context.SaveChangesAsync();
   }

   public async Task UnfollowOtherRekomerAsync(string rekomerId)
   {
      var accountPromise = _tokenService.GetRekomerAccountByReadingAccessToken();
      var followingPromise = _context.Rekomers.FindAsync(rekomerId);
      
      var follower = (await accountPromise).Rekomer;
      var following = await followingPromise;
      
      if (follower is null) { throw new YourProfileIsNotCreatedYetException(); }
      if (following is null || follower.Id == following.Id) { throw new NotFoundRekomerProfileException(); }

      var follow = await _context.Follows.SingleOrDefaultAsync(f => f.FollowerId == follower.Id && f.FollowingId == following.Id);

      if (follow is null) { throw new YouDidNotFollowThisRekomerYetException(); }

      _context.Follows.Remove(follow);
      await _context.SaveChangesAsync();
   }

   private async Task<List<TResult?>> GetFollowsAsync<TResult>(
      Expression<Func<Follow, bool>> predicate, 
      Expression<Func<Follow, TResult>> selector,
      int? page = null, int? limit = null)
   {
      var follows = _context.Follows
         .Where(predicate)
         .Select(selector)
         .AsQueryable();

      var totalFollow = follows.Count();

      if (limit is not null && page is not null) { follows = follows.Skip((int)((totalFollow / limit) * (page - 1))!).Take((int)limit); }

      return (await follows.ToListAsync())!;
   }

   public async Task<List<Rekomer?>> GetMyFollowersAsync(int? page = null, int? limit = null)
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid)!;
      
      return await GetFollowsAsync<Rekomer>(
         follow => follow.FollowingId == accountId,
         follow => follow.Follower!,
         page,
         limit
      );
   }
   
   public async Task<List<Rekomer?>> GetMyFollowingsAsync(int? page = null, int? limit = null)
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid)!;
      
      return await GetFollowsAsync<Rekomer>(
         follow => follow.FollowerId == accountId,
         follow => follow.Following!,
         page,
         limit
      );
   }
   
   public async Task<List<Rekomer?>> GetOtherFollowersAsync(string rekomerId, int? page = null, int? limit = null)
   {
      return await GetFollowsAsync<Rekomer>(
         follow => follow.FollowingId == rekomerId,
         follow => follow.Follower!,
         page,
         limit
      );
   }
   
   public async Task<List<Rekomer?>> GetOtherFollowingsAsync(string rekomerId, int? page = null, int? limit = null)
   {
      return await GetFollowsAsync<Rekomer>(
         follow => follow.FollowerId == rekomerId,
         follow => follow.Following!,
         page,
         limit
      );
   }
}