using System;
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

                Console.ReadLine();

        }

        static void Main() => RunClientAsync().Wait();
    }
}
