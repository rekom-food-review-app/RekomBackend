namespace RekomBackend.App.Services.CommonService;

public interface IMailService
{
   public Task SendEmailToConfirmAccountAsync(string email, string otp);
}