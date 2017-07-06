using System;
using System.Collections.Generic;
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
            var client = new Client("127.0.0.1", 6789, log, "Test");
            client.Start();

            Console.ReadLine();
        }
    }
}
