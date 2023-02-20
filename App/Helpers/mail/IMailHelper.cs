namespace RekomBackend.App.Helpers;

public interface IMailHelper
{
   public void SendEmail(MailDataBuilder emailData);
   public Task SendEmailAsync(MailDataBuilder emailData);
}