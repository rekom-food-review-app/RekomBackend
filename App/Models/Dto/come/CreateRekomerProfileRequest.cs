using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Models.Dto;

public class CreateRekomerProfileRequest
{
   [MinLength(1, ErrorMessage = "The Avatar field is required.")]
   public string FullName { get; set; } = null!;

   public IFormFile Avatar { get; set; } = null!;

   public DateTime Dob { get; set; }
}