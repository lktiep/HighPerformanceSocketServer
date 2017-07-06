using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network.Receive
{
    class R0006TimeResponse : ClientReceivePacket
    {
        public override void Read()
        {
            Log.Info($"{Connection.UserName}: Receive packet");
        }
    }
}
