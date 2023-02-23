using RekomBackend.App.Models.Dto;

namespace RekomBackend.App.Services.CommonService;

public interface IAccountService
{
   public Task<bool> ConfirmAccountAsync(ConfirmAccountRequest confirmRequest);
}