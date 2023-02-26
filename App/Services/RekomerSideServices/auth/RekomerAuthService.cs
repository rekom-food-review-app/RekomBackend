using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerAuthService : IRekomerAuthService
{
   private readonly RekomContext _context;
   private readonly IJwtHelper _jwtHelper;

   public RekomerAuthService(RekomContext context, IJwtHelper jwtHelper)
   {
      _context = context;
      _jwtHelper = jwtHelper;
   }

   public AuthToken CreateAuthToken(Rekomer rekomer)
   {
      if (rekomer.Account is null) { throw new NotIncludeAccountInRekomerException(); }
      
      var claims = new List<Claim>
      {
         new (ClaimTypes.Role, rekomer.Account.RoleEnum.ToString()),
         new (ClaimTypes.Sid, rekomer.Id)
      };
      
      return new AuthToken
      {
         AccessToken = _jwtHelper.CreateToken(claims, DateTime.Now.AddMonths(1)),
         RefreshToken = _jwtHelper.CreateToken(claims, DateTime.Now.AddMonths(1)),
      };
   }
   
   public async Task<AuthToken?> AuthWithEmailAsync(RekomerAuthEmailRequestDto authRequest)
   {
      var foundAccount = await _context.Accounts
         .Where(acc => 
            string.Equals(acc.Email, authRequest.Email.ToLower())
            && acc.PasswordHash == authRequest.Password
            && acc.RoleEnum == RoleEnum.Rekomer)
         .Include(acc => acc.Rekomer)
         .SingleOrDefaultAsync();

      return foundAccount is null ? null : CreateAuthToken(foundAccount.Rekomer!);
   }
}