using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace RekomBackend.App.Helpers;

public class MailHelper : IMailHelper
{
   private readonly IOptions<MailHostSetting> _mailHostSetting;
   
   public MailHelper(IOptions<MailHostSetting> mailHostSetting)
   {
      _mailHostSetting = mailHostSetting;
   }

   private void SendPrivate(MimeMessage email)
   {
      using var smtp = new SmtpClient();
      smtp.Connect(_mailHostSetting.Value.Host, _mailHostSetting.Value.Port);
      smtp.Authenticate(_mailHostSetting.Value.Username, _mailHostSetting.Value.Password);
      smtp.Send(email);
      smtp.Disconnect(true);
      email.Dispose();
   }

   private MimeMessage CreateMimeMessage(MailDataBuilder emailData)
   {
      var mime = new MimeMessage();
      mime.From.Add(new MailboxAddress(_mailHostSetting.Value.DisplayName, emailData.Sender ?? _mailHostSetting.Value.MailHostAddress));
      emailData.Receivers.ForEach(receiver => mime.To.Add(MailboxAddress.Parse(receiver)));
      mime.Subject = emailData.Subject;
      mime.Body = new TextPart(TextFormat.Html) {Text = emailData.Body};
      return mime;
   }
   
   public void SendEmail(MailDataBuilder emailData)
   {
      SendPrivate(CreateMimeMessage(emailData));
   }

   public Task SendEmailAsync(MailDataBuilder emailData)
   {
      return Task.Run(() => SendPrivate(CreateMimeMessage(emailData)));
   }
}