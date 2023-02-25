using RekomBackend.App.Models.Dto;
using RekomBackend.App.Models.Dto.RekomerSideDtos;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerProfileService
{
   public Task UpdateProfileAsync(PutRekomerProfileRequest putRequest);

   public Task<RekomerProfileResponse> GetMyProfileAsync();

   public Task<RekomerProfileResponse> GetOtherProfile(string rekomerId);
}