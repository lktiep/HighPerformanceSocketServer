using System;
using System.Text;
using Core;

namespace Server
{
    public class InternalClient : IInternalClient
    {
        private readonly Guid _guid;
        private readonly ILog _logger;


        public event ClientDisconnectedCallBack OnDisconnect;

        //public InternalClient(Guid guid, ILog logger, INode address, IConnection tcpConnection)
        //{
        //    _guid = guid;
        //    _logger = logger;
        //    _address = address;
        //    _tcpConnection = tcpConnection;

        //    _tcpConnection.OnDisconnection += OnTcpDisconnect;
        //    _tcpConnection.BeginReceive(ReceiveTCP);
        //}

        ///// <summary>
        ///// Khi client bị disconnect vì bất cứ lí do gì thì đây là event để handle
        ///// </summary>
        ///// <param name="reason"></param>
        ///// <param name="address"></param>
        //private void OnTcpDisconnect(Helios.Exceptions.HeliosConnectionException reason, IConnection address)
        //{
        //    Console.WriteLine("Disconnected: {0}; Reason: {1}", address.RemoteHost, reason.Type);
        //    _logger.Info($"Disconnected: {address.RemoteHost}; Reason: {reason.Type}");
            
        //    // Cleanup other connection here, then notify Disconnect to Server

        //    NotifyDisconnect();
        //}

        ///// <summary>
        ///// Nhận dữ liệu qua TCP
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="channel"></param>
        //public static void ReceiveTCP(NetworkData data, IConnection channel)
        //{
        //    var command = Encoding.UTF8.GetString(data.Buffer);

        //    //Console.WriteLine("Received: {0}", command);
        //    if (command.ToLowerInvariant() == "gettime")
        //    {
        //        var time = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());
        //        channel.Send(new NetworkData { Buffer = time, Length = time.Length, RemoteHost = channel.RemoteHost });
        //        //Console.WriteLine("Sent time to {0}", channel.Node);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid command: {0}", command);
        //        var invalid = Encoding.UTF8.GetBytes("Unrecognized command");

        //        channel.Send(new NetworkData
        //        {
        //            Buffer = invalid,
        //            Length = invalid.Length,
        //            RemoteHost = channel.RemoteHost
        //        });
        //    }
        //}

        /// <summary>
        /// Thông báo cho server biết Client này bị disconnect
        /// </summary>
        private void NotifyDisconnect()
        {
            OnDisconnect?.Invoke(_guid);
        }
    }
}