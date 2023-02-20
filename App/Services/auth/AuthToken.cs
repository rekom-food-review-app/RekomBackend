namespace RekomBackend.App.Services;

public class AuthToken
{
   public string AccessToken { get; init; } = null!;

   public string RefreshToken { get; init; } = null!;
}