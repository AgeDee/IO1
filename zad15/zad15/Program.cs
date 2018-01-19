using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zad15
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerTapClass server = new ServerTapClass(IPAddress.Any);
            ClientTapClass client1 = new ClientTapClass();
            var x = server.serverTaskRun();
            CancellationTokenSource cancellationToken1 = new CancellationTokenSource();
            client1.ConnectClient();
            var y = client1.keepPinging("Testowa wiadomosc", cancellationToken1.Token);

            cancellationToken1.CancelAfter(4000);
            Task.WaitAll(x, y);

        }
    }
    class ClientTapClass
    {
        TcpClient tcpClient;

        public void ConnectClient()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

        }

        public async Task<string> Ping(string msg)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(msg);
            tcpClient.GetStream().WriteAsync(buffer, 0, buffer.Length);
            buffer = new byte[1024];
            var t = await tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, t);
        }

        public async Task<IEnumerable<string>> keepPinging(string msg, CancellationToken cancellationToken)
        {
            List<string> messageList = new List<string>();
            bool operationEnded = false;
            while (!operationEnded)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    operationEnded = true;
                }
                messageList.Add(await Ping(msg));
            }
            return messageList;
        }



    }
    class ServerTapClass
    {
        TcpListener serverListener;
        Task taskServer;
        int port;
        IPAddress address;
        bool processRunning = false;

        public Task TaskServer
        {
            get
            {
                return taskServer;
            }
        }

        public IPAddress Address
        {
            get
            {
                return address;
            }
            set
            {
                if (!processRunning)
                {
                    address = value;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        CancellationTokenSource token = new CancellationTokenSource();
        public ServerTapClass(IPAddress ipAddress)
        {
            this.address = ipAddress;
            port = 2048;
        }

        async public Task serverTaskRun()
        {
            this.serverListener = new TcpListener(IPAddress.Any, 2048);
            serverListener.Start();
            while (true)
            {
                TcpClient tcpClient = await serverListener.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                await tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                    async (x) =>
                    {
                        int i = x.Result;
                        while (true)
                        {
                            tcpClient.GetStream().WriteAsync(buffer, 0, i);
                            i = await tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);
                            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, i));
                        }
                    });
            }
        }
    }
}
