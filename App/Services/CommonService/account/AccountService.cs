using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.Database;

namespace RekomBackend.App.Services.CommonService;

public class AccountService : IAccountService
{
   private readonly RekomContext _context;
   private readonly IOtpService _otpService;
   private readonly ITokenService _tokenService;
   
   public AccountService(RekomContext context, IOtpService otpService, ITokenService tokenService)
   {
      _context = context;
      _otpService = otpService;
      _tokenService = tokenService;
   }

   public async Task<bool> ConfirmAccountAsync(ConfirmAccountRequest confirmRequest)
   {
      var accountId = _tokenService.ReadClaimFromAccessToken(ClaimTypes.Sid);
      var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);
      
      if (account is null) { throw new InvalidAccessTokenException(); }
      
      if (account.IsConfirmed) { throw new AccountAlreadyConfirmedException(); }
      
      var isOtpVerifySuccessfully = await _otpService.VerifyOtpAsync(account.Id, confirmRequest.Otp);

      if (!isOtpVerifySuccessfully) return false;
      
      account.IsConfirmed = true;
      await _context.SaveChangesAsync();
      
      return true;
   }
}