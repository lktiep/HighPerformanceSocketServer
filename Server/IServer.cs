using System.Collections.Generic;
using System.Net;
using Core.Network;

namespace Server
{
    public interface IServer
    {
		string Host { get; }
        int Port { get; }

        void Start();
        void Stop();

        IEnumerable<IInternalClient> GetConnections();
        void BlockConnectionByUsername(string username);
        void UnblockConnectionByUsername(string username);
        void SendAll(string message);

		List<string> BanIps { get; }
		List<string> BanUsers { get; }

		PacketService PacketService { get; }
    }
}