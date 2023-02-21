using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services;

public interface IRekomerProfileService
{
   // public Task CreateProfileAsync(CreateRekomerProfileRequest createRequest);

   public Task<RekomerProfileResponse> GetMyProfileAsync();
}