using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services;

public interface IRegisterService
{
   public Task<bool> RegisterWithEmail(RegisterWithEmailRequest registerRequest);
}