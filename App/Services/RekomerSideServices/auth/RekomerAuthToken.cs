namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerAuthToken
{
   public string AccessToken { get; init; } = null!;
   public string RefreshToken { get; init; } = null!;
}