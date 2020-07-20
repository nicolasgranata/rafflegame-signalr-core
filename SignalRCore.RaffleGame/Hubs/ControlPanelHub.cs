using Microsoft.AspNetCore.SignalR;
using SignalRCore.DrawGame.Services;
using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public class ControlPanelHub : Hub<IControlPanelClient>
    {
        private readonly IDrawService _drawService;
        private readonly IHubContext<DrawHub> _drawHubContext;

        public ControlPanelHub(IDrawService drawService, IHubContext<DrawHub> drawHubContext)
        {
            _drawService = drawService;
            _drawHubContext = drawHubContext;
        }

        public async Task UpdateConnectedPlayers()
        {
            await Clients.All.UpdateConnectedPlayers(_drawService.GetParticipantsCount());
        }

        public async Task Draw()
        {
            var winner = _drawService.Draw();

            _drawService.RemoveAllParticipants();

            await Clients.Caller.GetWinner(winner);
            await _drawHubContext.Clients.Client(winner).SendAsync("Winner");
            await _drawHubContext.Clients.AllExcept(winner).SendAsync("Loser");
        }

        public async Task RestartGame()
        {
            _drawService.RemoveAllParticipants();

            await _drawHubContext.Clients.AllExcept(Context.ConnectionId).SendAsync("RestartGame");

            await Clients.Caller.UpdateConnectedPlayers(_drawService.GetParticipantsCount());
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.UpdateConnectedPlayers(_drawService.GetParticipantsCount());

            await base.OnConnectedAsync();
        }
    }
}
