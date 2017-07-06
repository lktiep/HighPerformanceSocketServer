using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network.Send
{
    public class S0006TimeResponse : ServerSendPacket
    {
        public S0006TimeResponse(IInternalClient connection) : base(connection)
        {
            Buffer.WriteInt16(1);
        }
    }
}
