using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Network;

namespace Client.Network.Receive
{
    public abstract class ClientReceivePacket
    {
        public ILog Log { get; set; }
        protected ByteBuffer Buffer;
        protected IClient Connection;

        public PacketService OpCodes;

        public void Process(IClient connection)
        {
            Buffer = new ByteBuffer(connection.Buffer);

            Connection = connection;
            Log = Connection.Log;
            OpCodes = connection.PacketService;

            try
            {
                Read();
            }
            catch (Exception ex)
            {
                Log?.Exception(ex, GetType().UnderlyingSystemType.Name);
            }
        }

        public abstract void Read();
    }
}
