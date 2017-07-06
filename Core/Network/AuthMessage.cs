using System;
using Hik.Communication.Scs.Communication.Messages;

namespace Core.Network
{
    [Serializable]
	public class AuthMessage : ScsMessage
	{
		public short OpCode;
		public byte[] Data;
	}
}