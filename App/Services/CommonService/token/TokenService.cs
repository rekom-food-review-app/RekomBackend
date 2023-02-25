﻿using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.Database;

namespace RekomBackend.App.Services.CommonService;

public class TokenService : ITokenService
{
   private readonly IJwtHelper _jwtHelper;
   // private readonly IConfiguration _configuration;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly RekomContext _context;
   
   // private readonly int _accessTokenExpireHours;
   // private readonly int _refreshTokenExpireMonths;

   public TokenService(IJwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor, RekomContext context)
   {
      _jwtHelper = jwtHelper;
      // _configuration = configuration;
      _httpContextAccessor = httpContextAccessor;
      _context = context;
      // _accessTokenExpireHours = _configuration.GetValue<int>("AccessTokenExpireHour");
      // _refreshTokenExpireMonths = _configuration.GetValue<int>("RefreshTokenExpireMonth");
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

      // var accessTokenExpireTime = DateTime.Now.AddHours(_accessTokenExpireHours); 
      // var refreshTokenExpireTime = DateTime.Now.AddMonths(_refreshTokenExpireMonths); 
      
      return new AuthToken
      {
         AccessToken = _jwtHelper.CreateToken(claims, DateTime.Now.AddMonths(1)),
         RefreshToken = _jwtHelper.CreateToken(claims, DateTime.Now.AddMonths(1)),
      };
   }
}