using Microsoft.AspNetCore.SignalR;

namespace KoiShow.APIService.Hubs
{
    public class KoiHub : Hub
    {
        public async Task SendPointUpdate(object data)
        {
            await Clients.All.SendAsync("ReceivePointUpdate", data);
        }

        public async Task SendContestResultUpdate(object data)
        {
            await Clients.All.SendAsync("ReceiveContestResultUpdate", data);
        }
    }
}