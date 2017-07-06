using System;
using Core;
using Core.Network;
using Server.Network.Send;

namespace Server
{
    public delegate void ClientDisconnectedCallBack(Guid clientGuid);

    public interface IInternalClient
    {
		Guid Guid { get; }
		string UserName { get;}
		string IP { get;}

		#region Internal Used

		byte[] Buffer { get; set; }
	    ILog Log { get; }
	    PacketService PacketService { get; }

	    #endregion

		event ClientDisconnectedCallBack OnDisconnect;
	    void Disconnect(string reason);
	    bool VerifyUsername(string username);
        void Send(ServerSendPacket packet);
    }
}