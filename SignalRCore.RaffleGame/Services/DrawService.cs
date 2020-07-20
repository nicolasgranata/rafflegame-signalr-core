using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRCore.DrawGame.Services
{
    public class DrawService : IDrawService
    {
        private IList<string> _userIds { get; set; }

        public DrawService()
        {
            _userIds = new List<string>();
        }

        public string Draw()
        {
            Random random = new Random();

            int index = random.Next(_userIds.Count());

            return _userIds.ElementAt(index);
        }

        public void AddParticipant(string participant)
        {
            _userIds.Add(participant);
        }

        public void RemoveParticipant(string participant)
        {
            _userIds.Remove(participant);
        }

        public void RemoveAllParticipants()
        {
            _userIds.Clear();
        }

        public int GetParticipantsCount() => _userIds.Count();

        public IList<string> GetParticipants() => _userIds;
    }
}
