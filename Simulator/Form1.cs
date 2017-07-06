﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;
using Core;
using NLog;

namespace Simulator
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = new NLogger(LogManager.GetCurrentClassLogger());
        private static string _host = "127.0.0.1";
        private static int _port = 6789;

        private List<IClient> _clients = new List<IClient>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(BuildClients);
        }

        private void BuildClients()
        {
            var numberOfConnections = int.Parse(textBox1.Text);

            Parallel.For(0, numberOfConnections, (i) =>
            {
                var client = new Client.Client(_host, _port, log, $"{i:D4}");

                _clients.Add(client);
                client.Start();
            });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (var client in _clients)
            {
                client.Stop();
            }

            _clients.Clear();
        }
    }
}
