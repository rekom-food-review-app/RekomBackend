using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Dto.RekomerSideDtos.Request;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerRegisterService
{
   public Task<RekomerAuthToken> RegisterWithEmailAsync(RekomerRegisterEmailRequestDto registerRequest);
}