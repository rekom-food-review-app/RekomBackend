﻿using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers.s3;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services.rekomer_follow;

public class RekomerFollowService : IRekomerFollowService
{
   private readonly RekomContext _context;
   private readonly ITokenService _tokenService;
   private readonly IS3Helper _s3Helper;
   
   public RekomerFollowService(RekomContext context, ITokenService tokenService, IS3Helper s3Helper)
   {
      _context = context;
      _tokenService = tokenService;
      _s3Helper = s3Helper;
   }
   
   // need to fix this
   private async Task<Account> GetRekomerAccountByReadingAccessToken()
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid);
      var account = await _context.Accounts
         .AsNoTracking()
         .Where(a => a.Id == accountId)
         .Include(a => a.Rekomer)
         .FirstOrDefaultAsync();
      
      if (account is null) { throw new InvalidAccessTokenException(); }
      
      return account;
   }

   public async Task FollowOtherRekomerAsync(string rekomerId)
   {
      var accountPromise = GetRekomerAccountByReadingAccessToken();
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
      var accountPromise = GetRekomerAccountByReadingAccessToken();
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

   public async Task<List<Rekomer?>> GetMyFollowers()
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid);
      
      var myFollowers = await _context.Follows
         .Where(f => f.FollowingId == accountId)
         .Select(f => f.Follower)
         .ToListAsync();
      
      return myFollowers;
   }
}