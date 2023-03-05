using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerFollowService : IRekomerFollowService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;

   public RekomerFollowService(RekomContext context, IMapper mapper)
   {
      _context = context;
      _mapper = mapper;
   }
   
   public async Task FollowAsync(string meId, string strangerId)
   {
      if (meId == strangerId) { throw new RekomerFollowYourSelfException(); }
      
      var stranger = await _context.Rekomers.AsNoTracking().SingleOrDefaultAsync(rek => rek.Id == strangerId);
      if (stranger is null) { throw new NotFoundRekomerException(); }

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
   
   public async Task UnFollowAsync(string meId, string followingId)
   {
      var following = await _context.Rekomers.AsNoTracking().SingleOrDefaultAsync(rek => rek.Id == followingId);
      if (following is null) throw new NotFoundRekomerException();

      var follow = await _context.Follows
         .AsNoTracking()
         .SingleOrDefaultAsync(fol => fol.FollowerId == meId && fol.FollowingId == followingId);
      if (follow is null) throw new RekomerNotAlreadyFollowException();

      _context.Follows.Remove(follow);
      await _context.SaveChangesAsync();
   }

   public async Task<IEnumerable<RekomerFollowerResponseDto>> GetFollowerListByRekomerAsync(
      string rekomerId, int page, int size, DateTime? lastTimestamp = null)
   {
      var rekomer = await _context.Rekomers.FindAsync(rekomerId);
      if (rekomer is null) throw new NotFoundRekomerException();

      var followerListQuery = _context.Follows
         .Where(fol => fol.FollowingId == rekomerId)
         .OrderByDescending(fol => fol.CreatedAt)
         .Include(fol => fol.Follower)
         .AsQueryable();
      
      if (lastTimestamp.HasValue) followerListQuery = followerListQuery.Where(fol => fol.CreatedAt < lastTimestamp.Value);

      return followerListQuery
         .Skip((page - 1) * size)
         .Take(size)
         .ToList()
         .Select(rek => _mapper.Map<RekomerFollowerResponseDto>(rek));
   }
   
   // public async Task<IEnumerable<RekomerFollowResponseDto>> GetFollowingListByRekomerAsync(
   //    string rekomerId, int page, int size, DateTime? lastTimestamp = null)
   // {
   //    var rekomer = await _context.Rekomers.FindAsync(rekomerId);
   //    if (rekomer is null) throw new NotFoundRekomerException();
   //
   //    var followerListQuery = _context.Follows
   //       .Where(fol => fol.FollowerId == rekomerId)
   //       .OrderByDescending(fol => fol.CreatedAt)
   //       .Include(fol => fol.Following!)
   //       .AsQueryable();
   //    
   //    if (lastTimestamp.HasValue) followerListQuery = followerListQuery.Where(fol => fol.CreatedAt < lastTimestamp.Value);
   //
   //    return followerListQuery
   //       .Skip((page - 1) * size)
   //       .Take(size)
   //       .ToList()
   //       .Select(rek => _mapper.Map<RekomerFollowResponseDto>(rek));
   // }
}