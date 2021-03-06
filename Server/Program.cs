﻿using System;
using System.Configuration;
using System.Threading.Tasks;
using Core;
using NLog;

namespace Server
{
    class Program
    {
        private static readonly ILog log = new NLogger(LogManager.GetCurrentClassLogger());
        private static string _host = "127.0.0.1";
        private static int _port = 6789;

        static async Task RunClientAsync()
        {

            var server = new Server(log, ConfigurationManager.AppSettings["host"], int.Parse(ConfigurationManager.AppSettings["port"]));
            server.Start();

            Console.ReadLine();

        }

        static void Main() => RunClientAsync().Wait();
    }
}
