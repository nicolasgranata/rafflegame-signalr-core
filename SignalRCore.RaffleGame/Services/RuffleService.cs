using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRCore.DrawGame.Services
{
    public class RuffleService : IRuffleService
    {
        private IList<string> userIds { get; set; }

        public RuffleService()
        {
            userIds = new List<string>();
        }

        public string Draw()
        {
            Random random = new Random();

            int index = random.Next(userIds.Count());

            return userIds.ElementAt(index);
        }

        public void AddParticipant(string participant)
        {
            userIds.Add(participant);
        }

        public void RemoveParticipant(string participant)
        {
            userIds.Remove(participant);
        }

        public void RemoveAllParticipants()
        {
            userIds.Clear();
        }
    }
}
