using System.ComponentModel.DataAnnotations;
using RekomBackend.App.Common.Enums;

namespace RekomBackend.App.Models.Dto;

public class RegisterWithEmailRequest
{
   [MinLength(1, ErrorMessage = "required")]
   public string Username { get; set; } = null!;

   [EmailAddress]
   public string Email { get; set; } = null!;

   [MinLength(6, ErrorMessage = "at least 6 characters")]
   public string Password { get; set; } = null!;

   public Role Role { get; set; }
}