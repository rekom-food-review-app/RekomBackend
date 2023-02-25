using RekomBackend.App.Dto.RekomerSideDtos;
using RekomBackend.App.Entities;

namespace RekomBackend.App.Services.RekomerSideServices;

public interface IRekomerAuthService
{
   public Task<AuthToken?> AuthWithEmailAsync(RekomerAuthEmailRequestDto authRequest);

   public AuthToken CreateAuthToken(Rekomer rekomer);
}