using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01Review
{
    class Program
    {
        static int inputInteger()
        {
            while (true)
            {
                try
                {
                    int value = Convert.ToInt32(Console.ReadLine());
                    return value;
                } catch (FormatException ex)
                {
                    Console.WriteLine("Invalid input, try again to enter a valid integer value.");
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter minimum: ");
                // int min = Convert.ToInt32(Console.ReadLine());
                //string minStr = Console.ReadLine();
                // int min = Convert.ToInt32(minStr);
                int min = inputInteger();

                Console.Write("Enter maximum: ");
                string maxStr = Console.ReadLine();
                int max = Convert.ToInt32(maxStr);

                if (min > max)
                {
                    Console.WriteLine("Error: minimum can not be larger than maximum");
                    // Environment.Exit(1);
                    return;
                }

                Console.Write("Enter how many numbers to generate: ");
                string countStr = Console.ReadLine();
                int count = Convert.ToInt32(countStr);
                // int count = int.Parse(countStr);
                if (count < 1)
                {
                    Console.WriteLine("Error: you must generate 1 or more numbers");
                    return;
                }

                Console.Write("Numbers generated: ");
                Random random = new Random();
                for (int n = 0; n < count; n++)
                {
                    int r = random.Next(min, max + 1);
                    Console.Write((n == 0 ? "" : ", ") + r);
                }
                Console.WriteLine();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: Value entered must be an integer");
            }
            finally
            {
                Console.WriteLine("Press any key to end");
                Console.ReadKey();
            }

        }
    }
}
