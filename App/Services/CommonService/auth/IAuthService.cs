using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services.CommonService;

public interface IAuthService
{
   public Task<AuthToken?> AuthWithEmailAsync(AuthWithEmailRequest authRequest);
}