using RekomBackend.App.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerOtpService : IRekomerOtpService
{
   private readonly RekomContext _context;
   private readonly int _expireTimeSeconds;

   public RekomerOtpService(RekomContext context, IConfiguration configuration)
   {
      _context = context;
      _expireTimeSeconds = configuration.GetValue<int>("OtpExpireSecond");
   }

   public async Task<Otp> CreateOtpAsync(string accountId)
   {
      var otp = new Otp
      {
         Id = Guid.NewGuid().ToString(),
         Code = new Random().Next(1000, 9999).ToString(),
         Expiration = DateTime.Now.AddSeconds(_expireTimeSeconds),
         AccountId = accountId
      };
      _context.Otps.Add(otp);
      await _context.SaveChangesAsync();
      return otp;
   }
}