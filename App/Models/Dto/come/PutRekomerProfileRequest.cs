using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Models.Dto;

public class PutRekomerProfileRequest
{
   [MinLength(1, ErrorMessage = "The Avatar field is required.")]
   public string FullName { get; set; } = null!;

   public IFormFile Avatar { get; set; } = null!;

   public DateTime? Dob { get; set; } = DateTime.Now;

   public string Description { get; set; } = null!;
}