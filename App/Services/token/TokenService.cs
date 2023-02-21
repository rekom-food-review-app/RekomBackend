using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.App.Models.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class TokenService : ITokenService
{
   private readonly IJwtHelper _jwtHelper;
   private readonly IConfiguration _configuration;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly RekomContext _context;
   
   private readonly int _accessTokenExpireHours;
   private readonly int _refreshTokenExpireMonths;

   public TokenService(IJwtHelper jwtHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, RekomContext context)
   {
      _jwtHelper = jwtHelper;
      _configuration = configuration;
      _httpContextAccessor = httpContextAccessor;
      _context = context;
      _accessTokenExpireHours = _configuration.GetValue<int>("AccessTokenExpireHour");
      _refreshTokenExpireMonths = _configuration.GetValue<int>("RefreshTokenExpireMonth");
   }

   public string? ReadClaimFromAccessToken(string name)
   {
      return _httpContextAccessor.HttpContext?.User.FindFirstValue(name);
   }
   
   public async Task<Account> GetRekomerAccountByReadingAccessToken()
   {
      var accountId = ReadClaimFromAccessToken(ClaimTypes.Sid);
      var account = await _context.Accounts
         .Where(a => a.Id == accountId)
         .Include(a => a.Rekomer)
         .FirstOrDefaultAsync();
      
      if (account is null) { throw new InvalidAccessTokenException(); }
      
      return account;
   }
   
   public AuthToken CreateAuthToken(Account account)
   {
      var claims = new List<Claim>
      {
         new (ClaimTypes.Name, account.Username),
         new (ClaimTypes.Role, account.Role.ToString()),
         new (ClaimTypes.Sid, account.Id)
      };

      var accessTokenExpireTime = DateTime.Now.AddHours(_accessTokenExpireHours); 
      var refreshTokenExpireTime = DateTime.Now.AddMonths(_refreshTokenExpireMonths); 
      
      return new AuthToken
      {
         AccessToken = _jwtHelper.CreateToken(claims, accessTokenExpireTime),
         RefreshToken = _jwtHelper.CreateToken(claims, refreshTokenExpireTime),
      };
   }
}