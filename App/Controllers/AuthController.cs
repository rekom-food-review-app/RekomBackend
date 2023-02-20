using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
   private readonly IAuthService _authService;

   public AuthController(IAuthService authService)
   {
      _authService = authService;
   }

   [HttpPost("email")]
   public async Task<IActionResult> AuthWithEmail(AuthWithEmailRequest authRequest)
   {
      try
      {
         var authToken = await _authService.AuthWithEmail(authRequest);

         if (authToken is null)
         {
            return BadRequest(new
            {
               code = "AF",
               message = "Authenticate Field."
            });
         }

         return Ok(new
         {
            code = "AS",
            message = "Authenticate Successfully.",
            authToken
         });
      }
      catch (NotFoundAccountException e)
      {
         return BadRequest(new
         {
            code = "NFA",
            message = "Can Not Find Your Account."
         });
      }
   }
}