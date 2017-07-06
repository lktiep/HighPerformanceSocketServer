using System;
using System.Collections.Generic;

namespace Core.Network
{
	public class PacketService
	{
		protected Dictionary<int, Type> RecvPackets = new Dictionary<int, Type>();
		protected Dictionary<Type, int> SendPackets = new Dictionary<Type, int>();

		/// <summary>
		/// Có packet gửi từ Client lên hay không
		/// </summary>
		/// <param name="opCode"></param>
		/// <returns></returns>
		public bool HasRecvPacket(int opCode)
		{
			return RecvPackets.ContainsKey(opCode);
		}

		public Type GetRecvPacketType(int opcode)
		{
			Type result;
			RecvPackets.TryGetValue(opcode, out result);
			return result;
		}

		public bool HasSendPacket(Type type)
		{
			return SendPackets.ContainsKey(type);
		}

		public int GetSendPacketOpcode(Type type)
		{
			int result;
			SendPackets.TryGetValue(type, out result);
			return result;
		}
	}
}
