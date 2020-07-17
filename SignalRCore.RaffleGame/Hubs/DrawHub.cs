using Microsoft.AspNetCore.SignalR;
using SignalRCore.DrawGame.Services;
using System;
using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public class DrawHub : Hub
    {
        private readonly IRuffleService _ruffleService;
        private readonly IHubContext<ControlPanelHub, IControlPanelClient> _controlPanelHubContext;

        public DrawHub(IRuffleService ruffleService, IHubContext<ControlPanelHub, IControlPanelClient> controlPanelHubContext)
        {
            _ruffleService = ruffleService;
            _controlPanelHubContext = controlPanelHubContext;
        }

        public async Task JoinParticipant()
        {
            _ruffleService.AddParticipant(Context.ConnectionId);
            await _controlPanelHubContext.Clients.All.UpdateConnectedPlayers(_ruffleService.GetParticipantsCount());
        }

        public void RefreshUsers()
        {
           // Clients.All.refreshUsers(UserHandler.ConnectedIds.Count().ToString());
        }

        public async Task RestartGame()
        {
            await base.OnDisconnectedAsync(null);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _ruffleService.RemoveParticipant(Context.ConnectionId);

            await _controlPanelHubContext.Clients.AllExcept(Context.ConnectionId).UpdateConnectedPlayers(_ruffleService.GetParticipantsCount());

            await base.OnDisconnectedAsync(exception);
        }
    }
}
