using Hik.Communication.Scs.Communication.Messages;

namespace Server.Network
{
	public class AuthMessage : ScsMessage
	{
		public short OpCode;
		public byte[] Data;
	}
}