using RekomBackend.App.Services.RekomerSideServices;

namespace RekomBackend.App.Dto.RekomerSideDtos.Response;

public class RekomerAuthResponseDto
{
   public RekomerAuthToken AuthToken { get; set; } = null!;

   public RekomerBasicProfile Profile { get; set; } = null!;
}