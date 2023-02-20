using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services;

public interface IMailService
{
   public Task SendEmailToConfirmAccountAsync(string email, string otp);
}