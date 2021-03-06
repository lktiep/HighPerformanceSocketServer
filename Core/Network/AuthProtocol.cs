using System;
using System.Collections.Generic;
using System.IO;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;

namespace Core.Network
{
	public class AuthProtocol : IScsWireProtocol
	{
		protected MemoryStream Stream = new MemoryStream();

		public byte[] GetBytes(IScsMessage message)
		{
			return ((AuthMessage) message).Data;
		}

		public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
		{
			Stream.Write(receivedBytes, 0, receivedBytes.Length);

			var messages = new List<IScsMessage>();

			while (ReadMessage(messages)) ;

			return messages;
		}

		private bool ReadMessage(List<IScsMessage> messages)
		{
			Stream.Position = 0;

			if (Stream.Length < 4)
				return false;

			var headerBytes = new byte[4];
			Stream.Read(headerBytes, 0, 4);

			var opCode = BitConverter.ToInt16(headerBytes, 0);
			int length = BitConverter.ToUInt16(headerBytes, 2);

			if (Stream.Length < length)
				return false;

			var message = new AuthMessage
			{
				OpCode = opCode,
				Data = new byte[length]
			};

			Stream.Read(message.Data, 0, message.Data.Length);

			messages.Add(message);

			TrimStream();

			return true;
		}

		public void Reset()
		{
		}

		private void TrimStream()
		{
			if (Stream.Position == Stream.Length)
			{
				Stream = new MemoryStream();
				return;
			}

			var remaining = new byte[Stream.Length - Stream.Position];
			Stream.Read(remaining, 0, remaining.Length);
			Stream = new MemoryStream();
			Stream.Write(remaining, 0, remaining.Length);
		}
	}
}