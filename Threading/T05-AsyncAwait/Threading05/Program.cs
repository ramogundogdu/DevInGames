using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetCurrentProcessorNumber();

    
        static void Main(string[] args)
        {
            Task[] workers = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                workers[i] = Knecht(i);
            }

            for (int i = 0; i < 20; i++)
            {
                int nProc = GetCurrentProcessorNumber();
                Console.WriteLine("Main (" + nProc + "): " + i);
                Thread.Sleep(100);
            }

            Console.WriteLine("Waiting for all Knechts to finish");

            Task.WaitAll(workers);

            Console.WriteLine("All Knechts are done");

            Console.ReadKey();
        }

        private static async Task Knecht(int iThread)
        {
            await Task.Run(delegate()
            {
                for (int i = 0; i < 100; i++)
                {
                    int nProc = GetCurrentProcessorNumber();
                    Console.WriteLine("Knecht (" + iThread + " on " + nProc + "): " + i);
                    Thread.Sleep(100);
                }
            });
        }
    }
}
