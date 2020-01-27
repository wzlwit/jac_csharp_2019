using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01Find
{
    class Program
    {

        const int length = 5;

        static List<double> numList = new List<double>();
        static double sum, avg;

        static void EnterValues()
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("Enter floating point value: ");
                string line = Console.ReadLine();
                double value;
                if (!double.TryParse(line, out value))
                {
                    Console.WriteLine("Invalid input. Exiting.");
                    Environment.Exit(1); // return is not enough to exit the program
                }
                numList.Add(value);
            }
        }

        static void FindSmallestNumber()
        {
            // int smallest = int.MaxValue;
            double smallest = numList[0];
            foreach (double num in numList)
            {
                if (num < smallest)
                {
                    smallest = num;
                }
            }
            Console.WriteLine("Smallest number is: " + smallest);
        }
        static void FindSumOfAllNumbers()
        {
            sum = 0.0;
            foreach (double num in numList)
            {
                sum += num;
            }
            Console.WriteLine("Sum of all numbers is: " + sum);
        }
        static void FindAverage()
        {
            avg = sum / numList.Count;
            Console.WriteLine("Average of all numbers is: " + avg);
        }
        static void FindStandardDeviation()
        {
            double sumOfDiffSquared = 0.0;
            foreach (double num in numList)
            {
                sumOfDiffSquared += (num - avg) * (num - avg);
            }
            double stdDev = Math.Sqrt(sumOfDiffSquared / numList.Count);
            Console.WriteLine("Standard deviation is: " + stdDev);
        }

        static void Main(string[] args)
        {
            try
            {
                EnterValues();
                FindSmallestNumber();
                FindSumOfAllNumbers();
                FindAverage();
                FindStandardDeviation();
            } finally
            {
                Console.WriteLine("Press any key to end");
                Console.ReadKey();
            }
        }
    }
}
