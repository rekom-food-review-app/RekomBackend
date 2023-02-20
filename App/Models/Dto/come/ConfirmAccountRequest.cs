using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Models.Dto;

public class ConfirmAccountRequest
{
   [MinLength(4), MaxLength(4)]
   public string Otp { get; set; } = null!;
}