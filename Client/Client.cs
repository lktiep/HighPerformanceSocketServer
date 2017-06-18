using System;
using System.Collections.Generic;
using System.Net;
using Helios.Exceptions;
using Helios.Net;
using Helios.Net.Bootstrap;
using Helios.Topology;

namespace Client
{
    public class Client : IClient
    {
        public IConnection Connection;
        public INode RemoteHost;

        public Client()
        {
        }

        public void Start(string ipAddress, int port)
        {
            try
            {
                RemoteHost = NodeBuilder.BuildNode().Host(ipAddress).WithPort(port).WithTransportType(TransportType.Tcp);
                Connection =
                    new ClientBootstrap()
                        .SetTransport(TransportType.Tcp)
                        .RemoteAddress(RemoteHost)
                        .OnConnect(OnTCPConnect)
                        .OnReceive(OnTCPReceive)
                        .OnDisconnect(OnTCPDisconnect)
                        .Build().NewConnection(NodeBuilder.BuildNode().Host(IPAddress.Any).WithPort(10001), RemoteHost);

                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnTCPConnect(INode remoteaddress, IConnection server)
        {
            Console.WriteLine($"Connected to {remoteaddress.Host}:{remoteaddress.Port}, start receiving data");
            server.BeginReceive();
        }

        private void OnTCPReceive(NetworkData data, IConnection server)
        {
            Console.WriteLine($"Receive {data.Length} from {server.RemoteHost}");
        }

        private void OnTCPDisconnect(HeliosConnectionException reason, IConnection server)
        {
            Console.WriteLine($"Disconnected from {server.RemoteHost}, reason: {reason}");
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }

    public interface IClient
    {
        void Start(string ipAddress, int port);
        void Stop();

        void Send(string message);
    }
}
