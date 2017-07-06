using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Network.Send;

namespace Server.Network.Receive
{
    public class R0005TimeRequest : ServerReceivePacket
    {
        public override void Read()
        {
            try
            {
                //var chatLength = Buffer.ReadInt16();
                //var chat = Buffer.ReadString(chatLength);

                Log.Info($"[{Connection.UserName}] received time request");

                Connection.Send(new S0006TimeResponse(Connection));
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }
    }
}
