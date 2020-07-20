namespace SignalRCore.DrawGame.Services
{
    public interface IDrawService
    {
        string Draw();

        void AddParticipant(string participant);

        void RemoveParticipant(string participant);

        void RemoveAllParticipants();

        int GetParticipantsCount();
    }
}
