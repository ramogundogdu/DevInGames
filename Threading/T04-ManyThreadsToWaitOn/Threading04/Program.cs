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

        public static AutoResetEvent[] autoEvents;
        
        static void Main(string[] args)
        {
            autoEvents = new AutoResetEvent[10];
            Thread[] workerThreads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                autoEvents[i] = new AutoResetEvent(false);
                workerThreads[i] = new Thread(Knecht);
                workerThreads[i].Start(i);
            }

            for (int i = 0; i < 20; i++)
            {
                int nProc = GetCurrentProcessorNumber();
                Console.WriteLine("Main (" + nProc + "): " + i);
                Thread.Sleep(100);
            }

            Console.WriteLine("Waiting for all Knechts to finish");

            WaitHandle.WaitAll(autoEvents);

            Console.WriteLine("All Knechts are done");

            Console.ReadKey();
        }

        private static void Knecht(object iO)
        {
            int iThread = (int) iO;
            for (int i = 0; i < 100; i++)
            {
                int nProc = GetCurrentProcessorNumber();
                Console.WriteLine("Knecht (" + iThread + " on " + nProc + "): " + i);
                Thread.Sleep(100);
            }
            autoEvents[iThread].Set();
        }
    }
}
