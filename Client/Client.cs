using System;
using Core;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;

namespace Client
{
    public class Client : IClient
    {
	    private IScsClient _client;
	    public string IpAddress { get; }
	    public int Port { get; }
		public ILog Log { get; }

		public Client(string ipAddress, int port, ILog log)
	    {
		    IpAddress = ipAddress;
		    Port = port;
		    Log = log;
	    }

        public void Start()
        {
			try
			{
				Log.Warn("Connecting to server at {0} : {1} ", Category.System, IpAddress, Port);
				_client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(IpAddress, Port));

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
			throw new NotImplementedException();
		}

		private void OnConnected(object sender, EventArgs e)
		{
			// Send first packet
		}

		private void OnDisconected(object sender, EventArgs e)
		{
			Log.Info($"Disconnected client at {_client.ToString()}");
		}

		public void Stop()
        {
			_client.Disconnect();

		}

        public void Send(string message)
        {
            throw new NotImplementedException();
        }

    }
}
