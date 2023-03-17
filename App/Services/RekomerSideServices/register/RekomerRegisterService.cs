using AutoMapper;
using NpgsqlTypes;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerRegisterService : IRekomerRegisterService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;
   private readonly IRekomerOtpService _otpService;
   private readonly IRekomerMailService _mailService;
   private readonly IRekomerAuthService _authService;

   public RekomerRegisterService(RekomContext context, IMapper mapper, IRekomerOtpService otpService, IRekomerMailService mailService, IRekomerAuthService authService)
   {
      _context = context;
      _mapper = mapper;
      _otpService = otpService;
      _mailService = mailService;
      _authService = authService;
   }

   public async Task<RekomerAuthToken> RegisterWithEmailAsync(RekomerRegisterEmailRequestDto registerRequest)
   {
      var account = new Account
      {
         PasswordHash = registerRequest.Password,
         Role = Role.Rekomer,
         IsConfirmed = false
      };
      _mapper.Map(registerRequest, account);
      account.Rekomer = new Rekomer
      {
         Id = account.Id,
         AccountId = account.Id
      };
      
      // account.Rekomer.FullTextSearch = NpgsqlTsVector.Parse($"{account.Rekomer.Description} {account.Username}  {account.Rekomer.FullName}");
      
      _context.Accounts.Add(account);
      await _context.SaveChangesAsync();
      
      var otp = await _otpService.CreateOtpAsync(account.Id);
      _ = _mailService.SendEmailToConfirmAccountAsync(account.Email, otp.Code);

      return _authService.CreateAuthToken(account.Rekomer);
   }
}