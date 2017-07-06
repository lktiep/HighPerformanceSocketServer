using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network.Receive
{
    public class R0003Chat : ServerReceivePacket
    {
        public override void Read()
        {
            try
            {
                var chatLength = Buffer.ReadInt16();
                var chat = Buffer.ReadString(chatLength);

                Log.Info($"[{Connection.UserName}] chat: {chat}");
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }
    }
}
