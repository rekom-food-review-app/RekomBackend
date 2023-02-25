using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerProfileService : IRekomerProfileService
{
   private readonly RekomContext _context;
   private readonly IS3Helper _s3Helper;

   public RekomerProfileService(RekomContext context, IS3Helper s3Helper)
   {
      _context = context;
      _s3Helper = s3Helper;
   }

   public async Task UpdateProfileAsync(string meId, RekomerUpdateProfileRequestDto updateRequest)
   {
      var me = await _context.Rekomers.SingleOrDefaultAsync(rek => rek.Id == meId);
      if (me is null) { throw new InvalidAccessTokenException(); }

      var avatarUrl = _s3Helper.UploadOneFile(updateRequest.Avatar);
      me.FullName = updateRequest.FullName;
      me.AvatarUrl = avatarUrl;
      me.Description = updateRequest.Description;
      me.Dob = updateRequest.Dob;

      await _context.SaveChangesAsync();
   }
}