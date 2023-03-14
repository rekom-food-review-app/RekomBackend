using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerProfileService
{
   public Task<RekomerBasicProfile> UpdateMyProfileAsync(string meId, RekomerUpdateProfileRequestDto updateRequest);
   
   public Task<RekomerProfileDetailResponseDto> GetMyProfileDetailAsync(string meId);

   public Task<RekomerProfileDetailResponseDto> GetOtherProfileDetailAsync(string meId, string rekomerId);
}