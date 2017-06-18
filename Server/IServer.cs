using System.Collections.Generic;
using System.Net;

namespace Server
{
    public interface IServer
    {
        IPAddress Host { get; }
        int Port { get; }

        void Start();
        void Stop();

        IEnumerable<IInternalClient> GetConnections();
        void BlockConnectionByUsername(string username);
        void UnblockConnectionByUsername(string username);
        void SendAll(string message);
    }
}