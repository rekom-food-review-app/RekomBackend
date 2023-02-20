using RekomBackend.App.Models.Entities;

namespace RekomBackend.App.Services;

public interface ITokenService
{
   public AuthToken CreateAuthToken(Account account);

   public string? ReadClaimFromAccessToken(string name);
}