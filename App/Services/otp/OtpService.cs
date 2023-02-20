using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class OtpService : IOtpService
{
   private readonly RekomContext _context;
   private readonly int _expireSecond;

   public OtpService(RekomContext context, IConfiguration configuration)
   {
      _context = context;
      _expireSecond = configuration.GetValue<int>("OtpExpireSecond");
   }

   public Task<Otp> CreateOtpAsync(Account account)
   {
      throw new NotImplementedException();
   }

   public async Task<Otp> CreateOtpAsync(string accountId)
   {
      var otp = new Otp
      {
         Id = Guid.NewGuid().ToString(),
         AccountId = accountId,
         Code = new Random().Next(1111, 9999).ToString(),
         Expiration = DateTime.Now.AddSeconds(_expireSecond)
      };

      _context.Otps.Add(otp);
      _ = await _context.SaveChangesAsync();

      return otp;
   }

   public async Task<bool> VerifyOtpAsync(string accountId, string otpCode)
   {
      var foundOtp = await _context.Otps.SingleOrDefaultAsync(
         o => o.AccountId == accountId && o.Code == otpCode);

      if (foundOtp is null) { return false; }

      _context.Otps.Remove(foundOtp);
      await _context.SaveChangesAsync();
      
      return true;
   }
}