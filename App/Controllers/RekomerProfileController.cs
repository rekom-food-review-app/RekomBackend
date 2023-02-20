using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Exceptions;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("rekomers")]
public class RekomerProfileController : ControllerBase
{
   private readonly IRekomerService _rekomerService;

   public RekomerProfileController(IRekomerService rekomerService)
   {
      _rekomerService = rekomerService;
   }
   
   [HttpPost("me/profile"), Authorize(Roles = "Rekomer")]
   public async Task<IActionResult> CreateProfile([FromForm] CreateRekomerProfileRequest createRequest)
   {
      try
      {
         await _rekomerService.CreateProfileAsync(createRequest);
      
         return Ok(new
         {
            code = "CPS",
            message = "Create Profile Successfully."
         });
      }
      catch (InvalidAccessTokenException e)
      {
         return Unauthorized();
      }
      catch (RekomerProfileIsAlreadyCreatedException e)
      {
         return BadRequest(new
         {
            code = "PAC",
            message = "Your Profile Is Already Created."
         });
      }
   }
}