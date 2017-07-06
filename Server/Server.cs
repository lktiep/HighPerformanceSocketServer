using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core;
using Core.Network;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using Server.Network;

namespace Server
{
    public class Server : IServer
    {
        private readonly ILog _logger;
	    private readonly IScsServer _server;

		public string Host { get; }
        public int Port { get; }

        private Dictionary<Guid, IInternalClient> _clients;

		public List<string> BanIps { get; }
		public List<string> BanUsers { get; }
		public PacketService PacketService { get; }

		public Server(ILog logger, string ipAddress, int port)
        {
            _logger = logger;
            Port = port;
            Host = ipAddress;

            _clients = new Dictionary<Guid, IInternalClient>();
			PacketService = new TCPPacketService();
			BanIps = new List<string>();
			BanUsers = new List<string>();

	        BuildBanList();

			try
			{
				_logger.Info("Start ManageServer at {0}:{1}...", Category.System, Host, Port);
				_server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(Host, Port));

				_server.ClientConnected += OnConnected;
			}
			catch (Exception ex)
			{
				_logger?.Exception(ex, "Start ManagementServer");
			}
		}

	    private void BuildBanList()
	    {
		    // Add blocked IP or block username here
	    }

	    private void OnConnected(object sender, ServerClientEventArgs e)
	    {
			string ip = Regex.Match(e.Client.RemoteEndPoint.ToString(), "([0-9]+).([0-9]+).([0-9]+).([0-9]+)").Value;
			_logger.Info($"Channel {ip} connected!");

			if (VerifyBanIPs())
		    {
				_logger.Error($"This {ip} is blocked!");
				e.Client.Disconnect();
				return;
			}

			var client = new InternalClient(_logger, e.Client, ip, PacketService, this);
			client.OnDisconnect += Client_OnDisconnect;

			_clients.Add(client.Guid, client);
			_logger.Info($"Accept client from {ip}:{client.Guid}");
		}

		private void Client_OnDisconnect(Guid clientGuid)
		{
			if (_clients.ContainsKey(clientGuid))
			{
				_logger.Info($"Removed client {clientGuid} because it's disconnected");
				_clients.Remove(clientGuid);
			}
			else
			{
				_logger.Info($"Couldn't find client {clientGuid} to remove");
			}
		}

		private bool VerifyBanIPs()
	    {
		    return true;
	    }

	    public void Start()
        {
			_server.Start();
			_logger.Info("Start listening clients...");
        }
                
        public void Stop()
        {
	        try
	        {
				_logger.Info("Shutting down server...");
		        foreach (var client in _clients)
		        {
			        client.Value.Disconnect("Server is off");
		        }

		        _server.Stop();
		        _clients.Clear();
	        }
	        finally
	        {
		        _logger.Info("Server is terminated!");
	        }
        }

        public IEnumerable<IInternalClient> GetConnections()
        {
            return _clients.Values;
        }

        public void BlockConnectionByUsername(string username)
        {
			if (BanUsers.Contains(username))
			{
				BanUsers.Add(username.ToLower());
				_logger.Info($"Blocked connection by username:{username}");
			}
		}

        public void UnblockConnectionByUsername(string username)
        {
	        if (BanUsers.Contains(username))
	        {
		        BanUsers.RemoveAll(u => u.ToLower() == username.ToLower());
				_logger.Info($"Unblocked connection from username:{username}");
	        }
        }

        public void SendAll(string message)
        {
            throw new NotImplementedException();
        }

    }
}