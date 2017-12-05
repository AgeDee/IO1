using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress adress = IPAddress.Parse("127.0.0.1");
            int port = 2048;

            Server server = new Server(adress, port);
            var sTask = server.serverTask();


            try
            {
                //server.Address = IPAddress.Parse("127.0.0.4");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                //server.Port = 8080;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Task.WaitAll(sTask);
        }

    }

    class Server
    {
        IPAddress address;
        int port;
        bool running;

        public Server(IPAddress adress, int port)
        {
            this.Address = adress;
            this.Port = port;
        }
        
        public int Port { get => port; set { if (!running) port = value; else throw new Exception("Nie można zmienic portu, kiedy serwer jest uruchomiony."); } }
        public IPAddress Address { get => address; set { if (!running) address = value; else throw new Exception("Nie można zmienić adresu IP, kiedy serwer jest uruchomiony."); } }
        public bool Running { get => running; set => running = value; }

        public async Task serverTask()
        {
            TcpListener server = new TcpListener(Address, Port);
            server.Start();
            running = true;
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                    async (t) =>
                    {
                        int i = t.Result;
                        LogServera log = new LogServera();
                        while (true)
                        {
                            client.GetStream().WriteAsync(buffer, 0, i);
                            i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                            log.add(buffer);
                            log.print();
                        }
                    });
            }
        }
    }

    class LogServera
    {
        List<byte[]> log = new List<byte[]>();

        public void add(byte[] buff)
        {
            log.Add(buff);
        }

        public void print()
        {
            string result;

            log.ForEach(delegate (byte[] b)
            {
                result = System.Text.Encoding.UTF8.GetString(b);
                Console.WriteLine(result);
            });    
        }
    }

}
