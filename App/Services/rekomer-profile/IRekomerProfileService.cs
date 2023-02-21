using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services;

public interface IRekomerProfileService
{
   public Task UpdateProfileAsync(PutRekomerProfileRequest putRequest);

   public Task<RekomerProfileResponse> GetMyProfileAsync();
}