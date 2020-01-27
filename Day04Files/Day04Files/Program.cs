using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04Files
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter a line of text: ");
                string input = Console.ReadLine();
                input += Environment.NewLine + input + Environment.NewLine + input;
                File.WriteAllText("data.txt", input); // overrides all existing content


                string allContent = File.ReadAllText("data.txt");
                Console.WriteLine("read contents: \n" + allContent);

                string[] lines = File.ReadAllLines("data.txt");
                foreach (string l in lines)
                {
                    Console.WriteLine("Line: " + l);
                }

                File.WriteAllLines("data.txt", lines);

                //
                File.AppendAllText("data.txt", "here we go again\n");
                File.AppendAllLines("data.txt", new string[] { "llll", "mmmm" });

            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
