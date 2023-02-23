namespace RekomBackend.App.Services.CommonService;

public class AuthToken
{
   public string AccessToken { get; init; } = null!;
   public string RefreshToken { get; init; } = null!;
}