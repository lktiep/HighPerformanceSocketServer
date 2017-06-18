using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core;
using NLog;

namespace Server
{
    class Program
    {
        private static readonly ILog log = new NLogger(LogManager.GetCurrentClassLogger());

        static void Main(string[] args)
        {
            var host = IPAddress.Any;
            var port = 5678;

            Console.Title = "Server";
            Console.WriteLine("Starting server on {0}:{1}", host, port);


            IServer server = new Server(log, host, port);
            server.Start();

            Console.ReadLine();
        }
    }
}
