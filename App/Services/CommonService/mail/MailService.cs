using RekomBackend.App.Helpers;

namespace RekomBackend.App.Services.CommonService;

public class MailService : IMailService
{
   private readonly IMailHelper _mailHelper;

   public MailService(IMailHelper mailHelper)
   {
      _mailHelper = mailHelper;
   }

   public async Task SendEmailToConfirmAccountAsync(string emailAddress, string otp)
   {
      var mail = new MailDataBuilder();
      mail.SendTo(emailAddress);
      mail.SetBody($"hi, bitch {otp}");
      mail.SetSubject("yeahhhh");
      
      await _mailHelper.SendEmailAsync(mail);
   }
}