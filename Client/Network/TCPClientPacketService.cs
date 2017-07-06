using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Network.Send;
using Core.Network;

namespace Client.Network
{
    public class TCPClientPacketService : PacketService
    {
        public TCPClientPacketService()
        {
            SendPackets.Add(typeof(S0001Authenticate), 0x0001);
        }
    }
}
