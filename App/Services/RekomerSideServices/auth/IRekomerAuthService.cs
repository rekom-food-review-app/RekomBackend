using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Dto.RekomerSideDtos.Request;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerAuthService
{
   public Task<RekomerAuthToken?> AuthWithEmailAsync(RekomerAuthEmailRequestDto authRequest);

   public RekomerAuthToken CreateAuthToken(Rekomer rekomer);
}