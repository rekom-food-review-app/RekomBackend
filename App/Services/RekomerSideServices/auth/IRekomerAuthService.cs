using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Dto.RekomerSideDtos.Response;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerAuthService
{
   public Task<RekomerAuthResponseDto?> AuthWithEmailAsync(string ipAddress, RekomerAuthEmailRequestDto authRequest);

   public RekomerAuthToken CreateAuthToken(Rekomer rekomer);
}