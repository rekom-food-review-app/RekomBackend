using AutoMapper;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers.s3;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class RekomerProfileProfileService : IRekomerProfileService
{
   private readonly RekomContext _context;
   private readonly ITokenService _tokenService;
   private readonly IS3Helper _s3Helper;
   private readonly IMapper _mapper;
   
   public RekomerProfileProfileService(RekomContext context, ITokenService tokenService, IS3Helper s3Helper, IMapper mapper)
   {
      _context = context;
      _tokenService = tokenService;
      _s3Helper = s3Helper;
      _mapper = mapper;
   }
   
   public async Task UpdateProfileAsync(PutRekomerProfileRequest putRequest)
   {
      var account = await _tokenService.GetRekomerAccountByReadingAccessToken();
      // if (account.Rekomer is not null) { throw new RekomerProfileIsAlreadyCreatedException(); }
      
      var avatarUrl = await _s3Helper.UploadOneFileAsync(putRequest.Avatar);
      
      account.Rekomer = new Rekomer
      {
         Id = account.Id,
         AccountId = account.Id,
         FullName = putRequest.FullName,
         AvatarUrl = avatarUrl,
         Description = putRequest.Description,
         Dob = putRequest.Dob
      };
      
      await _context.SaveChangesAsync();
   }

   public async Task<RekomerProfileResponse> GetMyProfileAsync()
   {
      var account = await _tokenService.GetRekomerAccountByReadingAccessToken();

      var rekomer = account.Rekomer;

      return _mapper.Map<RekomerProfileResponse>(rekomer);
   }
}