using System;
using Client.Network;
using Client.Network.Receive;
using Client.Network.Send;
using Core;
using Core.Network;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using UltimateTimer;

namespace Client
{
    public class Client : IClient
    {
        private readonly ThreadPoolTimer _chatTimer;
        private IScsClient _client;
	    public string IpAddress { get; }
	    public int Port { get; }
		public ILog Log { get; }
        public string UserName { get; }
        public PacketService PacketService { get; set; }
        public byte[] Buffer { get; set; }

        public Client(string ipAddress, int port, ILog log, string userName)
	    {
		    IpAddress = ipAddress;
		    Port = port;
		    Log = log;
	        UserName = userName;
	        PacketService = new TCPClientPacketService();

            _chatTimer = ThreadPoolTimer.Create(SendingRandomChat);
        }

        public void Start()
        {
			try
			{
				Log.Warn("Connecting to server at {0} : {1} ", Category.System, IpAddress, Port);
				_client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(IpAddress, Port));

                _client.WireProtocol = new AuthProtocol();

				_client.MessageReceived += OnReceiveMessage;
				_client.Connected += OnConnected;
				_client.Disconnected += OnDisconected;
				_client.Connect();
			}
			catch (Exception ex)
			{
				Log?.Exception(ex, "Starting Client");
			}
		}

		private void OnReceiveMessage(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
		{
		    var message = (AuthMessage)e.Message;
		    Buffer = message.Data;

		    if (PacketService.HasRecvPacket(message.OpCode))
		    {
		        var packetType = PacketService.GetRecvPacketType(message.OpCode);
		        var packetHandler = (ClientReceivePacket)Activator.CreateInstance(packetType);

		        try
		        {
		            packetHandler.Process(this);
		        }
		        catch
		        {
		            Log.Error($"Error when handle packet {message.OpCode:X4}:{_client}");
		        }
		    }
		    else
		    {
		        Log.Error($"No Handler for OPC: {message.OpCode:X4}, Length={Buffer.Length}");
		    }
        }

		private void OnConnected(object sender, EventArgs e)
		{
			// Send first packet
            Send(new S0001Authenticate(this));

            // After 3 seconds, sending random chat
		    ThreadExtension.DelayAndExecute(StartSendingRandomChat, 3000);
        }

        #region Testing

        private void StartSendingRandomChat()
        {
            _chatTimer.SetTimer(DateTime.Now, 3000, 0);
        }

        private void SendingRandomChat()
        {
            Send(new S0003Chat(this));
        }

        #endregion

        private void OnDisconected(object sender, EventArgs e)
		{
			Log.Info($"Disconnected client at {_client}");
		}

		public void Stop()
        {
			_client.Disconnect();

		}

        public void Send(ClientSendPacket packet)
        {
            try
            {
                var message = packet.GetMessage();

                if (message != null)
                {
                    _client.SendMessage(message);
                    Log.Debug($"Client sends {message.OpCode:X4}-{packet.GetType().Name}");
                }
                else
                {
                    Log.Warn("Just send NULL packet from");
                }
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }
    }
}
