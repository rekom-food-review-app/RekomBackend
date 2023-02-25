using RekomBackend.App.Dto.RekomerSideDtos.Request;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerProfileService
{
   public Task UpdateProfileAsync(string meId, RekomerUpdateProfileRequestDto updateRequest);
}