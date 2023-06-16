using Microsoft.AspNetCore.SignalR;

namespace eRestaurant.SignalR
{
    public class PorukeHub : Hub
    {
        public async Task ProslijediPoruku(string p)
        {
            await Clients.Others.SendAsync("PosaljiPoruku", p);
        }
    }
}
