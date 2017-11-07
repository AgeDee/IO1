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
            while (true) ;
        }

        static void ThreadServer(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ThreadClientService, new object[] { client });
            }
        }
        static void ThreadClientService(Object stateInfo)
        {
            TcpClient client = (TcpClient)((object[])stateInfo)[0];
            byte[] buffer = new byte[1024];

            byte[] message = new ASCIIEncoding().GetBytes("wiadomosc od serwera");
            client.GetStream().Write(message, 0, message.Length);

            while (true)
            {
                client.GetStream().Read(buffer, 0, 1024);
                Console.WriteLine("S Dostałem wiadomość:  " + new string(new ASCIIEncoding().GetChars(buffer)));
            }
            client.Close();
        }

        static void ThreadClient(Object stateInfo)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            byte[] message = new ASCIIEncoding().GetBytes("wiadomoscc");
            client.GetStream().Write(message, 0, message.Length);
            message = new ASCIIEncoding().GetBytes("");
            client.GetStream().Read(message, 0, message.Length);
            Console.WriteLine("C Dostałem wiadomość:  " + new string(new ASCIIEncoding().GetChars(message)));
        }

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
