using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.CommonService;

public interface ITokenService
{
   public AuthToken CreateAuthToken(Account account);

   public string? ReadClaimFromAccessToken(string name);

   public Task<Account> GetRekomerAccountByReadingAccessToken();
}