using Microsoft.AspNetCore.SignalR;
using SignalRCore.DrawGame.Services;
using System;
using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public class DrawHub : Hub
    {
        private readonly IDrawService _drawService;
        private readonly IHubContext<ControlPanelHub, IControlPanelClient> _controlPanelHubContext;

        public DrawHub(IDrawService drawService, IHubContext<ControlPanelHub, IControlPanelClient> controlPanelHubContext)
        {
            _drawService = drawService;
            _controlPanelHubContext = controlPanelHubContext;
        }

        public async Task JoinParticipant()
        {
            _drawService.AddParticipant(Context.ConnectionId);
            await _controlPanelHubContext.Clients.All.UpdateConnectedPlayers(_drawService.GetParticipantsCount());
        }

        public async Task RestartGame()
        {
            await base.OnDisconnectedAsync(null);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _drawService.RemoveParticipant(Context.ConnectionId);

            await _controlPanelHubContext.Clients.AllExcept(Context.ConnectionId).UpdateConnectedPlayers(_drawService.GetParticipantsCount());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
