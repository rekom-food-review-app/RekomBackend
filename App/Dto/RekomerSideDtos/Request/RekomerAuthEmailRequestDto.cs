﻿using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerAuthEmailRequestDto
{
   [EmailAddress]
   public string Email { get; set; } = null!;

   [MinLength(6, ErrorMessage = "at least 6 characters")]
   public string Password { get; set; } = null!;
}