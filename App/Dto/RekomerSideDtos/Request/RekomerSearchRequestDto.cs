using System.ComponentModel.DataAnnotations;
using RekomBackend.App.Dto.Common.Request;
using RekomBackend.App.Helpers;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerSearchRequestDto : PaginationRequestDto
{
   [MinLength(2)]
   public string Keyword { get; set; } = null!;

   public Coordinates? Location { get; set; } = null!;
}