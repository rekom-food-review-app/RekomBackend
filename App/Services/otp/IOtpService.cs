using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services.otp;

public interface IOtpService
{
   public Task<Otp> CreateOtpAsync(Account account);
   
   public Task<Otp> CreateOtpAsync(string accountId);
}