using RekomBackend.App.Helpers;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerMailService : IRekomerMailService
{
   private readonly IMailHelper _mailHelper;

   public RekomerMailService(IMailHelper mailHelper)
   {
      _mailHelper = mailHelper;
   }

   public async Task SendEmailToConfirmAccountAsync(string emailAddress, string otp)
   {
      var mail = new MailDataBuilder();
      mail.SendTo(emailAddress);
      mail.SetBody($"hi, this is your otp: {otp}");
      mail.SetSubject("REKOM - Confirm Account");
      
      await _mailHelper.SendEmailAsync(mail);
   }

   public async Task SendEmailWelcome(string emailAddress)
   {
      var mail = new MailDataBuilder();
      mail.SendTo(emailAddress);
      mail.SetBody($"hi, welcome");
      mail.SetSubject("REKOM - Welcome");
      
      await _mailHelper.SendEmailAsync(mail);
   }
}