namespace SignalRCore.DrawGame.Services
{
    public interface IRuffleService
    {
        string Draw();

        void AddParticipant(string participant);

        void RemoveParticipant(string participant);

        void RemoveAllParticipants();

        int GetParticipantsCount();
    }
}
