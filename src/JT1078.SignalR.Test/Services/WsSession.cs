using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JT1078.SignalR.Test.Services
{
    public class WsSession
    {
        private ConcurrentDictionary<string, string> sessions;

        public WsSession()
        {
            sessions = new ConcurrentDictionary<string, string>();
        }

        public void TryAdd(string connectionId)
        {
            sessions.TryAdd(connectionId, connectionId);
        }

        public int GetCount()
        {
            return sessions.Count;
        }

        public void TryRemove(string connectionId)
        {
            sessions.TryRemove(connectionId,out _);
        }

        public List<string> GetAll()
        {
            return sessions.Keys.ToList();
        }
    }
}
