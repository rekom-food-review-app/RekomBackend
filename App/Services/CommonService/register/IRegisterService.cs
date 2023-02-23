using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services.CommonService;

public interface IRegisterService
{
   public Task<AuthToken> RegisterWithEmailAsync(RegisterWithEmailRequest registerRequest);
}