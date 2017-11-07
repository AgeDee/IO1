using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 100 });
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 200 });
            Thread.Sleep(1000);
        }
        static void ThreadProc(Object stateInfo)
        {
            int delay = (int)((object[])stateInfo)[0];
            Console.WriteLine("Czekam " + delay + " ms");
            Thread.Sleep(delay);
        }
    }
}
