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
        delegate int DelegateType(int n);
        static DelegateType delegateSilniaIt;
        static DelegateType delegateSilniaRek;
        static DelegateType delegateFibo;

        static int silniait(int n)
        {
            int wynik = 1;
            for (int i = 1; i<n; i++)
            {
                wynik *= i;
            }

            return wynik;
        }

        static int silniarek(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }
            else
            {
                return silniarek(n - 1) * n;
            }
        }

        static int fibo(int n)
        {
            int fib = 0;

            for(int i = 0; i < n; i++)
            {
                fib += i;
            }
            return fib;
        }

        static void Main(string[] args)
        {
            delegateSilniaIt = new DelegateType(silniait);
            delegateSilniaRek = new DelegateType(silniarek);
            delegateFibo = new DelegateType(fibo);

            IAsyncResult ar = delegateSilniaIt.BeginInvoke(7, null, null);
            IAsyncResult ar2 = delegateSilniaRek.BeginInvoke(7, null, null);
            IAsyncResult ar3 = delegateFibo.BeginInvoke(7, null, null);

            int result1 = delegateSilniaIt.EndInvoke(ar);
            int result2 = delegateSilniaRek.EndInvoke(ar2);
            int result3 = delegateFibo.EndInvoke(ar3);

            //Console.WriteLine(result1);
            Console.WriteLine(result2);
            //Console.WriteLine(result3);
            Thread.Sleep(5000);
        }

    }
}
