using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Controllers.RekomerSideControllers;

[ApiController]
[Route("rekomer-side/register")]
public class RekomerRegisterController : ControllerBase
{
   private readonly IRekomerRegisterService _registerService;

   public RekomerRegisterController(IRekomerRegisterService registerService)
   {
      _registerService = registerService;
   }

   [HttpPost("email")]
   public async Task<IActionResult> RegisterWithEmail([FromBody] RekomerRegisterEmailRequestDto registerRequest)
   {
      var authToken = await _registerService.RegisterWithEmailAsync(registerRequest);

      return Ok(new
      {
         code = "CAS",
         message = "Create new account successfully",
         authToken
      });
   }
}