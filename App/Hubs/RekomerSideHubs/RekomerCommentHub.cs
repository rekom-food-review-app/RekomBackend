using Microsoft.AspNetCore.SignalR;
using RekomBackend.App.Dto.RekomerSideDtos.Response;

namespace RekomBackend.App.Hubs.RekomerSideHubs;

public class RekomerCommentHub : Hub
{
   public async Task SendToAllAsync(RekomerCommentResponseDto comment)
   {
      await Clients.All.SendAsync("ReceiveComment", comment);
   }
}