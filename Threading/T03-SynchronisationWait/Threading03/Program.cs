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

        // public static AutoResetEvent autoEvent;
        
        static void Main(string[] args)
        {
            // autoEvent = new AutoResetEvent(false);
            Thread workerThread = new Thread(Knecht);
            workerThread.Start();

            for (int i = 0; i < 20; i++)
            {
                int nProc = GetCurrentProcessorNumber();
                Console.WriteLine("Main (" + nProc + "): " + i);
                Thread.Sleep(100);
            }

            Console.WriteLine("Waiting for Knecht to finish");
            // Wait for Knecht...
            // autoEvent.WaitOne();
            workerThread.Join();
            Console.WriteLine("Knecht is done");

            Console.ReadKey();
        }

        private static void Knecht()
        {
            for (int i = 0; i < 100; i++)
            {
                int nProc = GetCurrentProcessorNumber();
                Console.WriteLine("Knecht (" + nProc + "): " + i);
                Thread.Sleep(100);
            }
            // autoEvent.Set();
        }
    }
}
