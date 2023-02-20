using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services.otp;

public class OtpService : IOtpService
{
   private readonly RekomContext _context;
   private readonly int _expireSecond;
   // private readonly IConfiguration _configuration;

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
}