using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public interface IControlPanelClient
    {
        Task SendWinner(string winner);

        Task SendLosser(string losser);

        Task UpdateConnectedPlayers(int playerCount);

        Task RestartGame();
    }
}
