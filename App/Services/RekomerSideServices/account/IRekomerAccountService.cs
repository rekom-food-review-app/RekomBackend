using RekomBackend.App.Dto.RekomerSideDtos.Request;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerAccountService
{
   public Task<bool> ConfirmAccountAsync(string meId, RekomerConfirmAccountRequest confirmRequest);
}