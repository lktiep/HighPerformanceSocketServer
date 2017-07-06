using System;

namespace Server.Network.Receive
{
	public class R0001Authenticate : ReceivePacket
	{
		public override void Read()
		{
			try
			{
				var usernameLength = Buffer.ReadInt16();
				var username = Buffer.ReadString(usernameLength);

				// Verify username here, if not valid, then raise Disconect event
				if (Connection.VerifyUsername(username))
				{
					// Accept this connection
					Log.Info($"Authenticated the client at {Connection.IP} with username: {username}");
				}
				else
				{
					Connection.Disconnect($"Failed to authenticate connection by username:{username}");
				}
			}
			catch (Exception e)
			{
				Log.Exception(e);
			}
		}
	}
}
