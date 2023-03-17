using System.ComponentModel.DataAnnotations;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerCommentRequestDto
{
   [MinLength(1)]
   public string Content { get; set; } = null!;
}