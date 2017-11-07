using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab1_zad5
{
    class Program
    {
        static void Main(string[] args)
        {
            int wielkosc = 3;
            int fragment = 5;
            int[] tablica = new int[wielkosc];
            int suma = 0;

            Random rnd = new Random();
            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i] = rnd.Next(0, 100);
            }

            if(true)
            {
                ThreadPool.QueueUserWorkItem(ThreadSumator, new object[] { 0, wielkosc, suma, tablica });
            }
            else
            {
                //dzieli na watki
            }

            Console.Write("suma: " + suma + "\n");
            Thread.Sleep(1000);

            Console.Write("suma: " + suma + "\n");
        }

        static void ThreadSumator(Object stateInfo)
        {
            int from = (int)((object[])stateInfo)[0];
            int to = (int)((object[])stateInfo)[1];
            int suma = (int)((object[])stateInfo)[2];
            int[] tablica = (int[])((object[])stateInfo)[3];

            for (int i = from; i < to; i++)
            {
                suma += tablica[i];
            }
            
        }
    }
}
