using System.Security.Claims;

namespace RekomBackend.App.Helpers;

public interface IJwtHelper
{
   public string CreateToken(List<Claim> claims, DateTime expireTime);
}