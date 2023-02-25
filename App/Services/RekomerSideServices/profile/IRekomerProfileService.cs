using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerProfileService
{
   public Task UpdateProfileAsync(PutRekomerProfileRequest putRequest);

   public Task<RekomerProfileResponse> GetMyProfileAsync();

   public Task<RekomerProfileResponse> GetOtherProfile(string rekomerId);
}