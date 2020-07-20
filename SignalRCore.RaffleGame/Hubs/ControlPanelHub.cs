using Microsoft.AspNetCore.SignalR;
using SignalRCore.DrawGame.Services;
using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public class ControlPanelHub : Hub<IControlPanelClient>
    {
        private readonly IRuffleService _ruffleService;
        private readonly IHubContext<DrawHub> _drawHubContext;

        public ControlPanelHub(IRuffleService ruffleService, IHubContext<DrawHub> drawHubContext)
        {
            _ruffleService = ruffleService;
            _drawHubContext = drawHubContext;
        }

        public async Task UpdateConnectedPlayers()
        {
            await Clients.All.UpdateConnectedPlayers(_ruffleService.GetParticipantsCount());
        }

        public async Task GetWinner()
        {
            var winner = _ruffleService.Draw();

            _ruffleService.RemoveAllParticipants();

            await Clients.Caller.GetWinner("WinnerIs");
            await _drawHubContext.Clients.Client(winner).SendAsync("Winner");
            await _drawHubContext.Clients.AllExcept(winner).SendAsync("Loser");
        }

        public async Task RestartGame()
        {
            _ruffleService.RemoveAllParticipants();

            await _drawHubContext.Clients.AllExcept(Context.ConnectionId).SendAsync("RestartGame");

            await Clients.Caller.UpdateConnectedPlayers(_ruffleService.GetParticipantsCount());
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.UpdateConnectedPlayers(_ruffleService.GetParticipantsCount());

            await base.OnConnectedAsync();
        }
    }
}
