using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {

        static void Main(string[] args)
        {
            FileStream fs;
            fs = new FileStream("plik.txt", FileMode.Open);

            byte[] buffer = new byte[1024];
            fs.BeginRead(buffer, 0, buffer.Length, null, new object[] {});

            Thread.Sleep(10);
            fs.Close();
            Console.WriteLine("Zamknięto plik. buffer: " + new string(new ASCIIEncoding().GetChars(buffer)));

            Thread.Sleep(5000);
        }

    }
}
