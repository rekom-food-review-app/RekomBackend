using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.Database;

namespace RekomBackend.App.Services;

public class AuthService : IAuthService
{
   private readonly RekomContext _context;
   private readonly ITokenService _tokenService;
   
   public AuthService(RekomContext context, ITokenService tokenService)
   {
      _context = context;
      _tokenService = tokenService;
   }

   public async Task<AuthToken?> AuthWithEmail(AuthWithEmailRequest authRequest)
   {
      var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Email == authRequest.Email);

      if (account is null) { throw new NotFoundAccountException(); }

      return account.PasswordHash == authRequest.Password ? _tokenService.CreateAuthToken(account) : null;
   }
}