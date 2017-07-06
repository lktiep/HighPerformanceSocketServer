using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network.Send
{
    public class S0003Chat : ClientSendPacket
    {
        public S0003Chat(IClient connection) : base(connection)
        {
            var chat = $"Hello from {Connection.UserName}: {DateTime.Now:O}";

            Buffer.WriteInt16(chat.Length);
            Buffer.WriteString(chat);
        }
    }
}
