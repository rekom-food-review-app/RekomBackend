using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services;

public interface IAccountService
{
   public Task<bool> ConfirmAccountAsync(ConfirmAccountRequest confirmRequest);
}