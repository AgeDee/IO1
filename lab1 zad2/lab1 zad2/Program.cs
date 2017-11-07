using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab1_zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadServer);
            ThreadPool.QueueUserWorkItem(ThreadClient);
            ThreadPool.QueueUserWorkItem(ThreadClient);
        }
    
        static void ThreadServer(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
            }
        }

        static void ThreadClient(Object stateInfo)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            //byte[] message = new ASCIIEncoding().GetBytes("wiadomosc");
            //client.GetStream().Write(message, 0, message.Length);
        }
    }
}
