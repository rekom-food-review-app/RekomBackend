using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers.s3;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class RekomerService : IRekomerService
{
   private readonly RekomContext _context;
   private readonly ITokenService _tokenService;
   private readonly IS3Helper _s3Helper;
   
   public RekomerService(RekomContext context, ITokenService tokenService, IS3Helper s3Helper)
   {
      _context = context;
      _tokenService = tokenService;
      _s3Helper = s3Helper;
   }

   public async Task CreateProfileAsync(CreateRekomerProfileRequest createRequest)
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid);
      
      var account = await _context.Accounts
         .Where(a => a.Id == accountId)
         .Include(a => a.Rekomer)
         .FirstOrDefaultAsync();
      
      if (account is null) { throw new InvalidAccessTokenException(); }

      if (account.Rekomer is not null) { throw new RekomerProfileIsAlreadyCreatedException(); }

      var avatarUrl = await _s3Helper.UploadOneFileAsync(createRequest.Avatar);
      
      var rekomer = new Rekomer
      {
         Id = account.Id,
         AccountId = account.Id,
         FullName = createRequest.FullName,
         AvatarUrl = avatarUrl,
         Dob = createRequest.Dob
      };

      _context.Rekomers.Add(rekomer);
      await _context.SaveChangesAsync();
   }
}