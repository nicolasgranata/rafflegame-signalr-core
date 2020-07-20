using System.Threading.Tasks;

namespace SignalRCore.DrawGame.Hubs
{
    public interface IControlPanelClient
    {
        Task GetWinner(string winner);

        Task UpdateConnectedPlayers(int playerCount);

        Task RestartGame();
    }
}
