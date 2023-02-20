using AutoMapper;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class RegisterService : IRegisterService
{
   private readonly RekomContext _context;
   private readonly IMapper _mapper;
   private readonly IOtpService _otpService;
   private readonly IMailService _mailService;
   private readonly ITokenService _tokenService;
   
   public RegisterService(RekomContext context, IMapper mapper, IOtpService otpService, IMailService mailService, ITokenService tokenService)
   {
      _context = context;
      _mapper = mapper;
      _otpService = otpService;
      _mailService = mailService;
      _tokenService = tokenService;
   }

   public async Task<AuthToken> RegisterWithEmailAsync(RegisterWithEmailRequest registerRequest)
   {
      var account = _mapper.Map<Account>(registerRequest);
      account.PasswordHash = registerRequest.Password;
      
      _context.Accounts.Add(account);
      _ = await _context.SaveChangesAsync();

      var otp = await _otpService.CreateOtpAsync(account.Id);

      _ = _mailService.SendEmailToConfirmAccountAsync(account.Email, otp.Code);

      return _tokenService.CreateAuthToken(account);
   }
}