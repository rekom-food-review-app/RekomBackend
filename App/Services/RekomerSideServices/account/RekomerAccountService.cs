using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Exceptions;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerAccountService : IRekomerAccountService
{
   private readonly RekomContext _context;
   private readonly IRekomerOtpService _otpService;
   private readonly IRekomerMailService _mailService;

   public RekomerAccountService(RekomContext context, IRekomerOtpService otpService, IRekomerMailService mailService)
   {
      _context = context;
      _otpService = otpService;
      _mailService = mailService;
   }
   
   /// <param name="meId"></param>
   /// <param name="confirmRequest"></param>
   /// <returns></returns>
   /// <exception cref="InvalidAccessTokenException"></exception>
   /// <exception cref="AccountAlreadyConfirmedException"></exception>
   public async Task<bool> ConfirmAccountAsync(string meId, RekomerConfirmAccountRequest confirmRequest)
   {
      var me = await _context.Rekomers
         .Include(rek => rek.Account)
         .Where(rek => rek.Id == meId).SingleOrDefaultAsync();

      if (me is null) { throw new InvalidAccessTokenException(); }
      if (me.Account!.IsConfirmed) { throw new AccountAlreadyConfirmedException(); }

      var isOtpConfirmSuccessfully = await _otpService.ConfirmOtpAsync(meId, confirmRequest.OtpCode);
      if (!isOtpConfirmSuccessfully) return false;

      _ = _mailService.SendEmailWelcome(me.Account.Email);
      me.Account.IsConfirmed = true;
      await _context.SaveChangesAsync();
      return true;
   }
}