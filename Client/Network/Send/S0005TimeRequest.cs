using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network.Send
{
    public class S0005TimeRequest : ClientSendPacket
    {
        public S0005TimeRequest(IClient connection) : base(connection)
        {
            var chat = $"Hello from {Connection.UserName}: {DateTime.Now:O}";

            Buffer.WriteInt16(chat.Length);
            Buffer.WriteString(chat);

            Log.Info($"{Connection.UserName}:Sent request timing");
        }
    }
}
