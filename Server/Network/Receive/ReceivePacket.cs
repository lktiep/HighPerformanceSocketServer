using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Network;

namespace Server.Network.Receive
{
	public abstract class ReceivePacket
	{
		public ILog Log { get; set; }
		protected ByteBuffer Buffer;
		protected IInternalClient Connection;

		public PacketService OpCodes;

		public void Process(IInternalClient connection)
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
