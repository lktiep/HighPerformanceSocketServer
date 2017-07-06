using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Network;
using Server.Network.Receive;

namespace Server.Network
{
	public class TCPServerPacketService : PacketService
	{
		public TCPServerPacketService()
		{

            RecvPackets.Add(0x0001, typeof(R0001Authenticate));
            RecvPackets.Add(0x0003, typeof(R0003Chat));
		}
	}
}
