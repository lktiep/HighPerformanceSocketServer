using Client.Network.Send;
using Core;
using Core.Network;

namespace Client
{
	public interface IClient
	{
		ILog Log { get; }
		string IpAddress { get; }
		int Port { get; }
	    byte[] Buffer { get; }
	    PacketService PacketService { get; }
	    string UserName { get; }

        void Start();
		void Stop();

		void Send(ClientSendPacket packet);
	}
}