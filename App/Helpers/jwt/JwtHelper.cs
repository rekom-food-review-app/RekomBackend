using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RekomBackend.App.Helpers;

public class JwtHelper : IJwtHelper
{
   private readonly IConfiguration _configuration;
   
   public JwtHelper(IConfiguration configuration)
   {
      _configuration = configuration;
   }

   public string CreateToken(List<Claim> claims, DateTime expireTime)
   {
      var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecret")!));

      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      
      var unhandledToken = new JwtSecurityToken(
         claims: claims,
         expires: expireTime,
         signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(unhandledToken);
   }
}