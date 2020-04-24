using Microsoft.AspNetCore.SignalR;
using SignalRCore.DrawGame.Services;
using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public class DrawHub : Hub
    {
        private readonly IRuffleService _ruffleService;

        public DrawHub(IRuffleService ruffleService)
        {
            _ruffleService = ruffleService;
        }

        public void JoinParticipant()
        {
            _ruffleService.AddParticipant(Context.ConnectionId);
        }

        public void RefreshUsers()
        {
           // Clients.All.refreshUsers(UserHandler.ConnectedIds.Count().ToString());
        }

        public async Task RestartGame()
        {
            _ruffleService.RemoveAllParticipants();

            await Clients.All.SendAsync("restartGame");
        }

        public override Task OnConnectedAsync()
        {
            _ruffleService.RemoveParticipant(Context.ConnectionId);

            return base.OnConnectedAsync();
        }
    }
}
