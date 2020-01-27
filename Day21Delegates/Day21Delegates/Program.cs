using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21Delegates
{
    public class Program
    {
        // step 1: declare delegate type
        public delegate void LogMessageDelegate(string s);

        // step 2: declare a field of delegate type
        static LogMessageDelegate Logger; // initially null when empty

        // step 3: create one or more methods whose siginatures match that of the delegate
        static void LogToConsole(string msg)
        {
            Console.WriteLine("MSG: " + msg);
        }

        static void LogToConsoleFancy(string mmm)
        {
            Console.WriteLine("**********************: " + mmm);
        }

        static void LogToFile(string msg)
        {            
            File.AppendAllText("log.txt", "LOG: " + msg);
            Console.WriteLine("FYI: log written to file");
        }

        static void Main(string[] args)
        {
            try
            {
                Random random = new Random();

                // if (random.Next(2) != 0) Logger += new LogMessageDelegate(LogToConsole); // Ver A
                if (random.Next(2) != 0) Logger += LogToConsole; // Ver B
                if (random.Next(2) != 0) Logger += LogToFile;
                if (random.Next(2) != 0) Logger += LogToConsoleFancy;
                // //
                if (Logger != null)
                { // calling multicast
                    Logger("This is Sparta!");
                }
                
            } finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
