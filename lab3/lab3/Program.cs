using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {
        static void ConsolePrint(string text)
        {
            Console.WriteLine(text);
        }
        static void Main(string[] args)
        {

            Zadania zadania = new Zadania();
            Task task = Zadania.contentDownloader(new Uri("http://www.feedforall.com/sample.xml"));
            task.Wait();
        }
    }

    class Zadania
    {
        //Zadanie12
        struct TResultDataStructure
        {
            private int iProperty { get; set; }
            private int taskInt { get; set; }

            public TResultDataStructure(int taskInt, int iProperty)
            {
                this.iProperty = iProperty;
                this.taskInt = taskInt;
            }

        }

        //Zadanie13
        public void Zadanie13()
        {


            Task.Run(
                () =>
                {
                    bool Z2 = true;

                });

        }

        //Zadanie14
        public static async Task contentDownloader(Uri url)
        {
            WebClient webClient = new WebClient();
            try
            {
                string pageContent = await webClient.DownloadStringTaskAsync(url);
                Console.WriteLine(pageContent);
            }
            catch (WebException webException)
            {
                Console.WriteLine(webException.Message);
            }
        }
    }
}
