using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/auth")]
public class RekomerAuthController : ControllerBase
{
   private readonly IRekomerAuthService _authService;
   private readonly IHttpContextAccessor _httpContextAccessor;

   public RekomerAuthController(IRekomerAuthService authService, IHttpContextAccessor httpContextAccessor)
   {
      _authService = authService;
      _httpContextAccessor = httpContextAccessor;
   }

   [HttpPost("email")]
   public async Task<IActionResult> AuthWithEmail([FromBody] RekomerAuthEmailRequestDto authRequest)
   {
      try
      {
         var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
         var authResponse = await _authService.AuthWithEmailAsync(ipAddress, authRequest);

         if (authResponse is null)
         {
            return Unauthorized();
         }

         return Ok(new
         {
            code = "ASUC",
            message = "Authenticate successfully.",
            authResponse.AuthToken,
            authResponse.Profile
         });
      }
      catch (NotFoundReactionException)
      {
         return BadRequest();
      }
      catch (TooManyRequestException)
      {
         return StatusCode(429);
      }
   }
}