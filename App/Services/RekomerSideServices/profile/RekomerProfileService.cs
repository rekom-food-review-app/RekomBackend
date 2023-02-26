using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

   public RekomerProfileService(RekomContext context, IS3Helper s3Helper, IMapper mapper)
   {
      _context = context;
      _s3Helper = s3Helper;
      _mapper = mapper;
   }

   /// <param name="meId"></param>
   /// <param name="updateRequest"></param>
   /// <exception cref="InvalidAccessTokenException"></exception>
   public async Task UpdateMyProfileAsync(string meId, RekomerUpdateProfileRequestDto updateRequest)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) throw new InvalidAccessTokenException();

      var avatarUrl = _s3Helper.UploadOneFile(updateRequest.Avatar);
      me.FullName = updateRequest.FullName;
      me.AvatarUrl = avatarUrl;
      me.Description = updateRequest.Description;
      me.Dob = updateRequest.Dob;

      await _context.SaveChangesAsync();
   }

   /// <param name="meId"></param>
   /// <returns></returns>
   /// <exception cref="InvalidAccessTokenException"></exception>
   public async Task<RekomerProfileDetailResponseDto> GetMyProfileDetailAsync(string meId)
   {
      var meQueryable = _context.Rekomers
         .Include(rek => rek.Account)
         .Where(rek => rek.Id == meId)
         .AsQueryable();

      var me = await meQueryable.FirstOrDefaultAsync();
      if (me is null) throw new InvalidAccessTokenException();
      
      var profileResponse = new RekomerProfileDetailResponseDto
      {
         AmountReview = await meQueryable.Include(rek => rek.Reviews).CountAsync(),
         AmountFollower = await meQueryable.Include(rek => rek.Followers).CountAsync(),
         AmountFollowing = await meQueryable.Include(rek => rek.Followings).CountAsync(),
         Username = me.Account!.Username
      };
      _mapper.Map(me, profileResponse);
      
      return profileResponse;
   }
   
   public async Task<RekomerProfileDetailResponseDto> GetOtherProfileDetailAsync(string meId, string rekomerId)
   {
      var rekomerQueryable = _context.Rekomers
         .Include(rek => rek.Account)
         .Where(rek => rek.Id == rekomerId)
         .AsQueryable();

      var rekomer = await rekomerQueryable.FirstOrDefaultAsync();
      if (rekomer is null) throw new NotFoundRekomerException();

      var profileResponse = new RekomerProfileDetailResponseDto
      {
         AmountReview = await rekomerQueryable.Include(rek => rek.Reviews).CountAsync(),
         AmountFollower = await rekomerQueryable.Include(rek => rek.Followers).CountAsync(),
         AmountFollowing = await rekomerQueryable.Include(rek => rek.Followings).CountAsync(),
         Username = rekomer.Account!.Username,
         IsFollow = await _context.Follows.SingleOrDefaultAsync(fol => fol.FollowerId == meId && fol.FollowingId == rekomerId) is not null
      };
      _mapper.Map(rekomer, profileResponse);
      
      return profileResponse;
   }
}