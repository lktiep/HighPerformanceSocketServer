using System;
using Core;
using Core.Network;

namespace Client.Network.Send
{
    public class ClientSendPacket
    {
        public ILog Log { get; set; }
        protected ByteBuffer Buffer;
        protected IClient Connection;

        public PacketService OpCodes;

        public ClientSendPacket(IClient connection)
        {
            Connection = connection;
            Log = connection.Log;
            OpCodes = connection.PacketService;
            Buffer = new ByteBuffer();
        }

        public AuthMessage GetMessage()
        {
            if (Buffer == null)
            {
                return null;
            }

            if (OpCodes.HasSendPacket(GetType()))
            {
                var message = new AuthMessage
                {
                    OpCode = (short) OpCodes.GetSendPacketOpcode(GetType()),
                    Data = new byte[Buffer.Length + 4]
                };
                // Thêm 2 cho OpCode và 2 cho Length

                // Copy OpCode vào
                Array.Copy(BitConverter.GetBytes(message.OpCode), message.Data, 2);
                Array.Copy(BitConverter.GetBytes(Buffer.Length), 0, message.Data, 2, 2);
                Array.Copy(Buffer.GetByteArray(), 0, message.Data, 4, Buffer.Length);

                return message;
            }

            return null;
        }
    }
}
