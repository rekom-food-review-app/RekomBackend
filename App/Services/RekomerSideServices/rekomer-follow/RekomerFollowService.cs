using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Entities;
using RekomBackend.App.Services.CommonService;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFollowService : IRekomerFollowService
{
   private readonly RekomContext _context;
   private readonly ITokenService _tokenService;
   private readonly IMapper _mapper;
   
   public RekomerFollowService(RekomContext context, ITokenService tokenService, IMapper mapper)
   {
      _context = context;
      _tokenService = tokenService;
      _mapper = mapper;
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

   public async Task<IEnumerable<RekomerProfileResponse?>> GetMyFollowersAsync(int? page = null, int? limit = null)
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid)!;

      var followersQuery = _context.Follows
         .Where(f => f.FollowingId == accountId)
         .Include(f => f.Follower!.Account)
         .Select(f => f.Follower!)
         .AsQueryable();

      var totalFollow = followersQuery.Count();
      
      if (limit is not null && page is not null) { followersQuery = followersQuery.Skip((int)((totalFollow / limit) * (page - 1))!).Take((int)limit); }
      
      return (await followersQuery.ToListAsync()).Select(flr =>
      {
         var response = _mapper.Map<RekomerProfileResponse>(flr);
         response.IsFollowed = true;
         return response;
      });
   }

   public async Task<IEnumerable<RekomerProfileResponse?>> GetMyFollowingsAsync(int? page = null, int? limit = null)
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid)!;

      var followingsQuery = _context.Follows
         .Where(f => f.FollowerId == accountId)
         .Include(f => f.Following!.Account)
         .Select(f => f.Following!)
         .AsQueryable();

      var totalFollow = followingsQuery.Count();
      
      if (limit is not null && page is not null) { followingsQuery = followingsQuery.Skip((int)((totalFollow / limit) * (page - 1))!).Take((int)limit); }
      
      return (await followingsQuery.ToListAsync()).Select(flr => _mapper.Map<RekomerProfileResponse>(flr));
   }

   public async Task<IEnumerable<RekomerProfileResponse?>> GetOtherFollowersAsync(string rekomerId, int? page = null, int? limit = null)
   {
      var followersQuery = _context.Follows
         .Where(f => f.FollowingId == rekomerId)
         .Include(f => f.Follower!.Account)
         .Select(f => f.Follower!)
         .AsQueryable();

      var totalFollow = followersQuery.Count();
      
      if (limit is not null && page is not null) { followersQuery = followersQuery.Skip((int)((totalFollow / limit) * (page - 1))!).Take((int)limit); }
      
      return (await followersQuery.ToListAsync()).Select(flr => _mapper.Map<RekomerProfileResponse>(flr));
   }

   public async Task<IEnumerable<RekomerProfileResponse?>> GetOtherFollowingsAsync(string rekomerId, int? page = null, int? limit = null)
   {
      var followingsQuery = _context.Follows
         .Where(f => f.FollowerId == rekomerId)
         .Include(f => f.Following!.Account)
         .Select(f => f.Following!)
         .AsQueryable();

      var totalFollow = followingsQuery.Count();
      
      if (limit is not null && page is not null) { followingsQuery = followingsQuery.Skip((int)((totalFollow / limit) * (page - 1))!).Take((int)limit); }
      
      return (await followingsQuery.ToListAsync()).Select(flr => _mapper.Map<RekomerProfileResponse>(flr));
   }
}