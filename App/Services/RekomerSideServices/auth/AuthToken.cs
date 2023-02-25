namespace RekomBackend.App.Services.RekomerSideServices;

public class AuthToken
{
   public string AccessToken { get; init; } = null!;
   public string RefreshToken { get; init; } = null!;
}