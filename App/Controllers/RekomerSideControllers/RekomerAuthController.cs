using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/auth")]
public class RekomerAuthController : ControllerBase
{
   private readonly IRekomerAuthService _authService;

   public RekomerAuthController(IRekomerAuthService authService)
   {
      _authService = authService;
   }

   [HttpPost("email")]
   public async Task<IActionResult> AuthWithEmail([FromBody] RekomerAuthEmailRequestDto authRequest)
   {
      var authToken = await _authService.AuthWithEmailAsync(authRequest);

      if (authToken is null)
      {
         return Unauthorized();
      }

      return Ok(new
      {
         code = "ASUC",
         message = "Authenticate successfully.",
         authToken
      });
   }
}