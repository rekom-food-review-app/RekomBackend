using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerUpdateProfileRequestDto
{
   [MinLength(1, ErrorMessage = "required.")]
   public string FullName { get; set; } = null!;
   
   public IFormFile Avatar { get; set; } = null!;

   public string? Description { get; set; }

   public DateTime? Dob { get; set; }
}