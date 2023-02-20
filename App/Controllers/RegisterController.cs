﻿using Microsoft.AspNetCore.Mvc;
using RekomBackend.App.Models.Dto;
using RekomBackend.App.Services;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
   private readonly IRegisterService _registerService;

   public RegisterController(IRegisterService registerService)
   {
      _registerService = registerService;
   }

   [HttpPost("email")]
   public async Task<IActionResult> RegisterWithEmail(RegisterWithEmailRequest registerRequest)
   {
      var authToken = await _registerService.RegisterWithEmail(registerRequest);

      return StatusCode(201, new
      {
         code = "",
         messaage = "Your Account Is Created Successfully, Please Verify It By The OTP That We Sent You In Email.",
         authToken
      });
   }
}