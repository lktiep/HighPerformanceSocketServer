using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core;
using Helios.Net;
using Helios.Ops.Executors;
using Helios.Reactor;
using Helios.Reactor.Bootstrap;
using Helios.Topology;

namespace Server
{
    public class Server : IServer
    {
        private readonly ILog _logger;
        IReactor _server;

        public IPAddress Host { get; }
        public int Port { get; }

        private Dictionary<Guid, IInternalClient> _clients;

        public Server(ILog logger, IPAddress ipAddress, int port)
        {
            _logger = logger;
            Port = port;
            Host = ipAddress;

            _clients = new Dictionary<Guid, IInternalClient>();
        }

        public void Start()
        {
            var executor = new TryCatchExecutor(exception => Console.WriteLine("Unhandled exception: {0}", exception));

            var bootstrapper =
                new ServerBootstrap()
                    .WorkerThreads(2)
                    .Executor(executor)
                    .SetTransport(TransportType.Tcp)
                    .Build();

            _server = bootstrapper.NewReactor(NodeBuilder.BuildNode().Host(Host).WithPort(Port));
            _server.OnConnection += OnTcpConnection;

            _server.Start();

            Console.WriteLine("Running...");
        }
                
        private void OnTcpConnection(INode address, IConnection connection)
        {
            Console.WriteLine("Connected: {0}", address);

            var guid = Guid.NewGuid();
            IInternalClient internalClient = new InternalClient(guid, _logger, address, connection);
            internalClient.OnDisconnect += OnClientDisconnected;

            _clients.Add(guid, internalClient);
        }

        private void OnClientDisconnected(Guid clientGuid)
        {
            if (_clients.ContainsKey(clientGuid))
            {
                _clients.Remove(clientGuid);
            }
        }

        public void Stop()
        {
            _clients.Clear();

            Console.WriteLine("Shutting down...");
            _server.Stop();
            Console.WriteLine("Terminated");
        }

        public IEnumerable<IInternalClient> GetConnections()
        {
            return _clients.Values;
        }

        public void BlockConnectionByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void UnblockConnectionByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void SendAll(string message)
        {
            throw new NotImplementedException();
        }
    }
}