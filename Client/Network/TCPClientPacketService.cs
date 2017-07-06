using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Network.Receive;
using Client.Network.Send;
using Core.Network;

namespace Client.Network
{
    public class TCPClientPacketService : PacketService
    {
        public TCPClientPacketService()
        {
            SendPackets.Add(typeof(S0001Authenticate), 0x0001);
            SendPackets.Add(typeof(S0003Chat), 0x0003);
            SendPackets.Add(typeof(S0005TimeRequest), 0x0005);

            RecvPackets.Add(0x0006, typeof(R0006TimeResponse));
        }
    }
}
