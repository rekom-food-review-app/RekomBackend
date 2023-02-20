using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("account")]
[Authorize(Roles = "Rekomer")]
public class AccountController : ControllerBase
{
   private readonly IAccountService _accountService;

   public AccountController(IAccountService accountService)
   {
      _accountService = accountService;
   }

   [HttpPost("confirm")]
   public async Task<IActionResult> ConfirmAccount(ConfirmAccountRequest confirmRequest)
   {
      try
      {
         var isAccountConfirmSuccessfully = await _accountService.ConfirmAccountAsync(confirmRequest);

         if (isAccountConfirmSuccessfully)
         {
            return Ok(new
            {
               code = "CAS",
               message = "Confirm Account Successfully."
            });
         }
         
         return BadRequest(new
         {
            code = "CAF",
            message = "Invalid Otp Code."
         });
      }
      catch (InvalidAccessTokenException e)
      {
         return Unauthorized();
      }
      catch (AccountAlreadyConfirmedException e)
      {
         return BadRequest(new
         {
            code = "AAC",
            message = "Your Account Is Already Confirm."
         });
      }
   }
}