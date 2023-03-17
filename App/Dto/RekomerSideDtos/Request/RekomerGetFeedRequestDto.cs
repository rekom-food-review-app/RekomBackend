using RekomBackend.App.Dto.Common.Request;
using RekomBackend.App.Helpers;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerGetFeedRequestDto : PaginationRequestDto
{
   public Coordinates Location { get; set; } = null!;
}