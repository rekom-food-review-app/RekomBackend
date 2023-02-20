namespace RekomBackend.App.Helpers;

public class MailDataBuilder
{
   public List<string> Receivers { get; private set; } = new();
   // public List<string> BccReceivers { get; private set; } = new();
   // public List<string> CcReceivers { get; private set; } = new();
   public string? Sender { get; private set; }
   // public string? DisplayName { get; private set; }
   // public string? ReplyTo { get; private set; }
   // public string? ReplyToName { get; private set; }
   public string Subject { get; private set; } = null!;
   public string? Body { get; private set; }

   public MailDataBuilder SendTo(string email)
   {
      Receivers.Add(email);
      return this;
   }

   // public EmailDataBuilder SendToBcc(string email)
   // {
   //    BccReceivers.Add(email);
   //    return this;
   // }

   // public EmailDataBuilder SendToCc(string email)
   // {
   //    CcReceivers.Add(email);
   //    return this;
   // }

   public MailDataBuilder SendFrom(string email)
   {
      Sender = email;
      return this;
   }

   public MailDataBuilder SetSubject(string subject)
   {
      Subject = subject;
      return this;
   }

   public MailDataBuilder SetBody(string body)
   {
      Body = body;
      return this;
   }
}