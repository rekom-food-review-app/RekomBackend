using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services;

public interface IOtpService
{
   public Task<Otp> CreateOtpAsync(Account account);
   
   public Task<Otp> CreateOtpAsync(string accountId);
   
   public Task<bool> VerifyOtpAsync(string accountId, string otpCode);
}