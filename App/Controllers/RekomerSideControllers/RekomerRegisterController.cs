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
      try
      {
         var authToken = await _registerService.RegisterWithEmailAsync(registerRequest);

         return Ok(new
         {
            code = "CAS",
            message = "Create new account successfully",
            authToken
         });
      }
      catch (Exception exc)
      {
         return StatusCode(500, new
         {
            code = "ISE",
            message = "Something went wrong",
            exc
         });
      }
   }
}