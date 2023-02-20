using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services;

public interface IRekomerService
{
   public Task CreateProfileAsync(CreateRekomerProfileRequest createRequest);
}