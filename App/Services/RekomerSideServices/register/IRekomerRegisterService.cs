using RekomBackend.App.Dto.RekomerSideDtos;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRegisterService
{
   public Task<AuthToken> RegisterWithEmailAsync(RekomerRegisterEmailRequestDto registerRequest);
}