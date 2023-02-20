namespace RekomBackend.App.Helpers;

public class MailHostSetting
{
   public string DisplayName { get; set; } = null!;
   public string MailHostAddress { get; set; } = null!;
   public int Port { get; set; }
   public string Host { get; set; } = null!;
   public string Username { get; set; } = null!;
   public string Password { get; set; } = null!;
}