using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/account")]
[Authorize(Roles = "Rekomer")]
public class RekomerAccountController : ControllerBase
{
   private readonly IRekomerAccountService _accountService;
   private readonly IHttpContextAccessor _httpContextAccessor;

   
   public RekomerAccountController(IRekomerAccountService accountService, IHttpContextAccessor httpContextAccessor)
   {
      _accountService = accountService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpPost("confirm")]
   public async Task<IActionResult> ConfirmAccount([FromBody] RekomerConfirmAccountRequest confirmRequest)
   {
      try
      {
         var meId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Sid)!;
         var isAccountConfirmSuccessfully = await _accountService.ConfirmAccountAsync(meId, confirmRequest);

         if (isAccountConfirmSuccessfully)
         {
            return Ok(new
            {
               code = "CSF",
               message = "Confirm account successfully."
            });
         }
         
         return BadRequest(new
         {
            code = "CNSF",
            message = "Can not confirm your account."
         });
      }
      catch (AccountAlreadyConfirmedException)
      {
         return BadRequest(new
         {
            code = "AAC",
            message = "Account is already confirm"
         });
      }
   }
}