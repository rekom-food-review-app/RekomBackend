using RekomBackend.App.Helpers;

namespace RekomBackend.App.Dto.RekomerSideDtos.Request;

public class RekomerGetFeedsRequestDto
{
   public Coordinates Coordinate { get; set; } = null!;
}