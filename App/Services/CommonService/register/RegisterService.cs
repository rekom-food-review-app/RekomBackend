using AutoMapper;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Entities;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services.RekomerSideServices;
using RekomBackend.Database;

namespace RekomBackend.App.Services.CommonService;

public class RegisterService : IRegisterService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;
   private readonly IOtpService _otpService;
   private readonly IRekomerMailService _rekomerMailService;
   private readonly ITokenService _tokenService;
   
   public RegisterService(RekomContext context, IMapper mapper, IOtpService otpService, IRekomerMailService rekomerMailService, ITokenService tokenService)
   {
      _context = context;
      _mapper = mapper;
      _otpService = otpService;
      _rekomerMailService = rekomerMailService;
      _tokenService = tokenService;
   }

   public async Task<AuthToken> RegisterWithEmailAsync(RegisterWithEmailRequest registerRequest)
   {
      var account = _mapper.Map<Account>(registerRequest);
      account.PasswordHash = registerRequest.Password;

      _context.Accounts.Add(account);

      if (account.Role == Role.Rekomer)
      {
         var rekomerProfile = new Rekomer()
         {
            Id = account.Id,
            AccountId = account.Id,
            AvatarUrl = "55f3aeb4-ea58-4b7b-b928-4d9fdf5b7663."
         };

         account.Rekomer = rekomerProfile;
      }
      
      _ = await _context.SaveChangesAsync();

      var otp = await _otpService.CreateOtpAsync(account.Id);

      _ = _rekomerMailService.SendEmailToConfirmAccountAsync(account.Email, otp.Code);

      return _tokenService.CreateAuthToken(account);
   }
}