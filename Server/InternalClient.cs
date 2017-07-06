using System;
using System.Reflection.Emit;
using System.Text;
using Core;
using Core.Network;
using Hik.Communication.Scs.Server;
using Server.Network;
using Server.Network.Receive;

namespace Server
{
    public class InternalClient : IInternalClient
    {
	    private readonly IScsServerClient _client;
	    private readonly IServer _server;
	    public PacketService PacketService { get; }

		public ILog Log { get; }
		public Guid Guid { get; }
		public byte[] Buffer { get; set; }
		public string UserName { get; set; }
		public string IP { get; }

		public event ClientDisconnectedCallBack OnDisconnect;

		public InternalClient(ILog log, IScsServerClient client, string ip, PacketService packetService, IServer server)
		{
			Guid = new Guid();

			Log = log;
			_client = client;
			IP = ip;
			_server = server;
			PacketService = packetService;

			_client.WireProtocol = new AuthProtocol();
			_client.Disconnected += OnTCPDisconnected;
			_client.MessageReceived += OnReceiveMessage;
		}

		private void OnReceiveMessage(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
		{
			var message = (AuthMessage)e.Message;
			Buffer = message.Data;

			if (PacketService.HasRecvPacket(message.OpCode))
			{
				var packetType = PacketService.GetRecvPacketType(message.OpCode);
				var packetHandler = (ReceivePacket)Activator.CreateInstance(packetType);

				try
				{
					packetHandler.Process(this);
				}
				catch
				{
					Log.Error($"Error when handle packet {message.OpCode:X4}:{IP}");
				}
			}
			else
			{
				Log.Error($"No Handler for OPC: {message.OpCode:X4}, Length={Buffer.Length}");
			}
		}

		///// <summary>
		///// Khi client bị disconnect vì bất cứ lí do gì thì đây là event để handle
		///// </summary>
		private void OnTCPDisconnected(object sender, EventArgs e)
		{
			Log.Info($"Disconnected: {IP}");

			// Cleanup other connection here, then notify Disconnect to Server

			NotifyDisconnect();
		}

		/// <summary>
		/// Thông báo cho server biết Client này bị disconnect
		/// </summary>
		private void NotifyDisconnect()
        {
            OnDisconnect?.Invoke(Guid);
        }

	    public void Disconnect(string reason)
	    {
		    Log.Info($"Force disconnect client {Guid} at {IP} because {reason}");
			_client.Disconnect();
	    }

	    public bool VerifyUsername(string username)
	    {
		    if (_server.BanUsers.Contains(username.ToLower()))
		    {
			    return false;
		    }

		    UserName = username;
		    return true;
	    }

    }
}