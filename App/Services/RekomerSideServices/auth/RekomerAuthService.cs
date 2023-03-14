using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Helpers;
using RekomBackend.Database;

namespace RekomBackend.App.Services.RekomerSideServices;

public class RekomerAuthService : IRekomerAuthService
{
   private readonly RekomContext _context;
   private readonly IJwtHelper _jwtHelper;
   private readonly IRekomerAuthRateLimitService _rateLimitService;
   private readonly IMapper _mapper;

   public RekomerAuthService(RekomContext context, IJwtHelper jwtHelper, IRekomerAuthRateLimitService rateLimitService, IMapper mapper)
   {
      _context = context;
      _jwtHelper = jwtHelper;
      _rateLimitService = rateLimitService;
      _mapper = mapper;
   }

   public RekomerAuthToken CreateAuthToken(Rekomer rekomer)
   {
      if (rekomer.Account is null) { throw new NotIncludeAccountInRekomerException(); }
      
      var claims = new List<Claim>
      {
         new (ClaimTypes.Role, rekomer.Account.Role.ToString()),
         new (ClaimTypes.Sid, rekomer.Id)
      };
      
      return new RekomerAuthToken
      {
         AccessToken = _jwtHelper.CreateToken(claims, DateTime.Now.AddYears(1)),
         RefreshToken = _jwtHelper.CreateToken(claims, DateTime.Now.AddMonths(1)),
      };
   }
   
   public async Task<RekomerAuthResponseDto?> AuthWithEmailAsync(string ipAddress, RekomerAuthEmailRequestDto authRequest)
   {
      if (!(await _rateLimitService.IsAllowedAsync(ipAddress))) throw new TooManyRequestException();
      
      var foundAccount = await _context.Accounts
         .Where(acc => 
            string.Equals(acc.Email, authRequest.Email.ToLower())
            && acc.PasswordHash == authRequest.Password
            && acc.Role == Role.Rekomer)
         .Include(acc => acc.Rekomer)
         .SingleOrDefaultAsync();

      if (foundAccount is null) return null;
      if (foundAccount.Rekomer is null) throw new NotFoundReactionException();
      // return CreateAuthToken(foundAccount.Rekomer);
      return new RekomerAuthResponseDto
      {
         AuthToken = CreateAuthToken(foundAccount.Rekomer),
         Profile = _mapper.Map<RekomerBasicProfile>(foundAccount.Rekomer)
      };
   }
}