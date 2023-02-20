using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services.otp;

public interface IOtpService
{
   public Task<Otp> CreateOtp(Account account);
   
   public Task<Otp> CreateOtp(string accountId);
}