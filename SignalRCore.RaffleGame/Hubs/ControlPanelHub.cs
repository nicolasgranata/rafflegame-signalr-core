using Microsoft.AspNetCore.SignalR;
using SignalRCore.DrawGame.Services;
using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public class ControlPanelHub : Hub
    {
        private readonly IRuffleService _ruffleService;

        public ControlPanelHub(IRuffleService ruffleService)
        {
            _ruffleService = ruffleService;
        }

        public async Task GetWinner()
        {
            var winner = _ruffleService.Draw();

            _ruffleService.RemoveAllParticipants();

            await Clients.AllExcept(winner).SendAsync("loser");
            await Clients.Client(winner).SendAsync("winner");
        }
    }
}
