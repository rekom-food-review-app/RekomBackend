using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFollowService : IRekomerFollowService
{
   private readonly RekomContext _context;

   public RekomerFollowService(RekomContext context)
   {
      _context = context;
   }
   
   /// <param name="meId"></param>
   /// <param name="strangerId"></param>
   /// <exception cref="RekomerFollowYourSelfException"></exception>
   /// <exception cref="NotFoundRekomerProfileException"></exception>
   /// <exception cref="RekomerAlreadyFollowException"></exception>
   public async Task FollowAsync(string meId, string strangerId)
   {
      if (meId == strangerId) { throw new RekomerFollowYourSelfException(); }
      
      var stranger = await _context.Rekomers.AsNoTracking().SingleOrDefaultAsync(rek => rek.Id == strangerId);
      if (stranger is null) { throw new NotFoundRekomerProfileException(); }

      var isAlreadyFollow = (await _context.Follows
         .AsNoTracking()
         .SingleOrDefaultAsync(fol => fol.FollowerId == meId && fol.FollowingId == strangerId)) is not null;
      if (isAlreadyFollow) { throw new RekomerAlreadyFollowException(); }

      _context.Follows.Add(new Follow
      {
         Id = Guid.NewGuid().ToString(),
         FollowerId = meId,
         FollowingId = strangerId
      });
      await _context.SaveChangesAsync();
   }
   
   /// <param name="meId"></param>
   /// <param name="followingId"></param>
   /// <exception cref="NotFoundRekomerProfileException"></exception>
   /// <exception cref="RekomerNotAlreadyFollowException"></exception>
   public async Task UnFollowAsync(string meId, string followingId)
   {
      var following = await _context.Rekomers.AsNoTracking().SingleOrDefaultAsync(rek => rek.Id == followingId);
      if (following is null) { throw new NotFoundRekomerProfileException(); }

      var follow = await _context.Follows
         .AsNoTracking()
         .SingleOrDefaultAsync(fol => fol.FollowerId == meId && fol.FollowingId == followingId);
      if (follow is null) { throw new RekomerNotAlreadyFollowException(); }

      _context.Follows.Remove(follow);
      await _context.SaveChangesAsync();
   }
}