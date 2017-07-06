using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Core;
using NLog;

namespace Client
{
    class Program
    {
        private static readonly ILog log = new NLogger(LogManager.GetCurrentClassLogger());

        static void Main(string[] args)
        {
            var _host = ConfigurationManager.AppSettings["host"];
            var _port = int.Parse(ConfigurationManager.AppSettings["port"]);

            var client = new Client(_host, _port, log, "Test");
            client.Start();

            Console.ReadLine();
        }
    }
}
