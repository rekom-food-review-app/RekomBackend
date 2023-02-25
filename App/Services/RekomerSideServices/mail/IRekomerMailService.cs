namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerMailService
{
   public Task SendEmailToConfirmAccountAsync(string email, string otp);
}