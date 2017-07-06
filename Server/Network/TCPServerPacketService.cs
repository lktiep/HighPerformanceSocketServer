using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Network;
using Server.Network.Receive;
using Server.Network.Send;

namespace Server.Network
{
	public class TCPServerPacketService : PacketService
	{
		public TCPServerPacketService()
		{
            SendPackets.Add(typeof(S0006TimeResponse), 0x0006);

            RecvPackets.Add(0x0001, typeof(R0001Authenticate));
            RecvPackets.Add(0x0003, typeof(R0003Chat));
            RecvPackets.Add(0x0005, typeof(R0005TimeRequest));
		}
	}
}
