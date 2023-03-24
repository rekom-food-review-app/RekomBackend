using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerCreateReviewRequestDto
{
   [RegularExpression("^(1|2|3|4|5)$")]
   public string Rating { get; set; } = null!;

   [MinLength(1, ErrorMessage = "Require.")]
   public string Content { get; set; } = null!;
   
   [MinLength(1, ErrorMessage = "Please provide at least one image")]
   public List<IFormFile> Images { get; set; } = null!;
}