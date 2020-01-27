using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02Again
{
    namespace Internal
    {
        class Aaaa
        {
            // I am in Day02Again.Internal.Aaaa class
        }
    }
}

namespace Day01Hello
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter your name: ");
                String name = Console.ReadLine();
                Console.Write("Enter your age: ");
                String ageStr = Console.ReadLine();
                int age;
                if (int.TryParse(ageStr, out age) == false)
                {
                    Console.WriteLine("Error parsing age. Not a valid integer.");
                    return;
                }
                Console.WriteLine("Hi {0}, you are {1} y/o.", name, age);
                if (age < 18)
                {
                    Console.WriteLine("You are not an adult yet.");
                }
            }
            finally
            {
                Console.WriteLine("Press any key to end");
                Console.ReadKey();
            }
        }
    }
}
