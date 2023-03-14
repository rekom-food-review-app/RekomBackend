using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerProfileService : IRekomerProfileService
{
   private readonly RekomContext _context;
   private readonly IS3Helper _s3Helper;
   private readonly IMapper _mapper;
   private readonly ILogger<RekomerProfileService> _logger;

   public RekomerProfileService(RekomContext context, IS3Helper s3Helper, IMapper mapper, ILogger<RekomerProfileService> logger)
   {
      _context = context;
      _s3Helper = s3Helper;
      _mapper = mapper;
      _logger = logger;
   }

   /// <param name="meId"></param>
   /// <param name="updateRequest"></param>
   /// <exception cref="InvalidAccessTokenException"></exception>
   public async Task<RekomerBasicProfile> UpdateMyProfileAsync(string meId, RekomerUpdateProfileRequestDto updateRequest)
   {
      var me = await _context.Rekomers.Include(rek => rek.Account).SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var avatarUrl = _s3Helper.UploadOneFile(updateRequest.Avatar);
      me.FullName = updateRequest.FullName;
      me.AvatarUrl = avatarUrl;
      me.Description = updateRequest.Description;
      me.Dob = updateRequest.Dob;

      await _context.SaveChangesAsync();

      return _mapper.Map<RekomerBasicProfile>(me);
   }

   /// <param name="meId"></param>
   /// <returns></returns>
   /// <exception cref="InvalidAccessTokenException"></exception>
   public async Task<RekomerProfileDetailResponseDto> GetMyProfileDetailAsync(string meId)
   {
      var meQueryable = _context.Rekomers
         .Include(rek => rek.Account)
         .Include(rek => rek.Reviews)
         .Include(rek => rek.Followers)
         .Include(rek => rek.Followings)
         .Where(rek => rek.Id == meId)
         .AsQueryable();

      var me = await meQueryable.FirstOrDefaultAsync();
      if (me is null) throw new InvalidAccessTokenException();
      
      var profileResponse = new RekomerProfileDetailResponseDto
      {
         AmountReview = me.Reviews!.Count(),
         AmountFollower = me.Followers!.Count(),
         AmountFollowing = me.Followings!.Count(),
         Username = me.Account!.Username
      };
      _mapper.Map(me, profileResponse);
      
      return profileResponse;
   }
   
   /// <param name="meId"></param>
   /// <param name="rekomerId"></param>
   /// <returns></returns>
   /// <exception cref="NotFoundRekomerException"></exception>
   public async Task<RekomerProfileDetailResponseDto> GetOtherProfileDetailAsync(string meId, string rekomerId)
   {
      var rekomerQueryable = _context.Rekomers
         .Include(rek => rek.Account)
         .Include(rek => rek.Reviews)
         .Include(rek => rek.Followers)
         .Include(rek => rek.Followings)
         .Where(rek => rek.Id == rekomerId)
         .AsQueryable();

      var rekomer = await rekomerQueryable.FirstOrDefaultAsync();
      if (rekomer is null) throw new NotFoundRekomerException();

      var profileResponse = new RekomerProfileDetailResponseDto
      {
         AmountReview = rekomer.Reviews!.Count(),
         AmountFollower = rekomer.Followers!.Count(),
         AmountFollowing = rekomer.Followings!.Count(),
         Username = rekomer.Account!.Username,
         IsFollow = await _context.Follows.SingleOrDefaultAsync(fol => fol.FollowerId == meId && fol.FollowingId == rekomerId) is not null
      };
      _mapper.Map(rekomer, profileResponse);
      
      return profileResponse;
   }
}