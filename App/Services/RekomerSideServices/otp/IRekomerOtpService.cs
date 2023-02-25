using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerOtpService
{
   public Task<Otp> CreateOtpAsync(string accountId);

   public Task<bool> ConfirmOtpAsync(string accountId, string otpCode);
}