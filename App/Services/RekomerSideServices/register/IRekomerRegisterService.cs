namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRegisterService
{
   public Task<AuthToken> RegisterWithEmail();
}