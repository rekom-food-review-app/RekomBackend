using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services.mail;

public interface IMailService
{
   public Task SendEmailToConfirmAccountAsync(string email, string otp);
}