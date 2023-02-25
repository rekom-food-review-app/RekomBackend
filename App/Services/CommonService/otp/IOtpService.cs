using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.CommonService;

public interface IOtpService
{
   public Task<Otp> CreateOtpAsync(Account account);
   
   public Task<Otp> CreateOtpAsync(string accountId);
   
   public Task<bool> VerifyOtpAsync(string accountId, string otpCode);
}