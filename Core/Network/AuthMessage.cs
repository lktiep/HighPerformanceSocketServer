using Hik.Communication.Scs.Communication.Messages;

namespace Core.Network
{
	public class AuthMessage : ScsMessage
	{
		public short OpCode;
		public byte[] Data;
	}
}