using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03Homeworks
{
    class Program
    {

        public static void ReadInNames()
        {
            List<string> nameList = new List<string>();
            Console.WriteLine("Enter names, one per line. Empty line to finish");
            while (true)
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                if (name == "")
                {
                    break;
                    // continue;
                }
                nameList.Add(name);
            }
            foreach (String n in nameList)
            {
                Console.WriteLine(n);
            }
        }

        public static int[] RemoveDups(int []a1, int[] a2)
        {
            // part 1 - find out how many are non-duplicates
            int desiredSize = 0;
            for (int i = 0; i < a1.Length; i++)
            {
                int val = a1[i];
                bool isDup = false;
                for (int j = 0; j < a2.Length; j++)
                {
                    if (val == a2[j])
                    {
                        isDup = true;                        
                    }
                }
                if (!isDup)
                {
                    desiredSize++;
                }
            }
            //
            int resultPos = 0;
            int [] result = new int[desiredSize];
            for (int i = 0; i < a1.Length; i++)
            {
                int val = a1[i];
                bool isDup = false;
                for (int j = 0; j < a2.Length; j++)
                {
                    if (val == a2[j])
                    {
                        isDup = true;
                        break;
                    }
                }
                if (!isDup)
                {
                    result[resultPos] = val;
                    resultPos++;
                }
            }
            return result;
        }

        private static bool IsValIn2DArray(int val, int[,]data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (val == data[i, j]) return true;
                }
            }
            return false;
        }

        public static void PrintDups(int[,] a1, int[,]a2) {
            Console.Write("Duplicates: ");
            for (int i = 0; i < a1.GetLength(0); i++)
            {
                for (int j = 0; j < a1.GetLength(1); j++)
                {
                    if (IsValIn2DArray(a1[i,j], a2))
                    {
                        Console.Write("{0}, ", a1[i, j]);
                    }
                }
            }
            Console.WriteLine();
        }

        public static void PrintDupsForEach(int[,] a1, int[,] a2)
        {
            Console.Write("Duplicates: ");
            foreach (int v1 in a1)
            {
                foreach (int v2 in a2)
                {
                    if (v1 == v2)
                    { 
                        Console.Write("{0}, ", v1);
                    }
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            try
            {
                /*
                int[] res = RemoveDups(new int[] { 2, 3, 5, 7, 3, 2 }, new int[] { 3, 9, 7 });
                foreach (int v in res)
                {
                    Console.Write("{0}, ", v);
                }
                */
                /*
                PrintDups(new int[,] { { 2, 3, 5 }, { 7, 3, 2 } }, new int[,] { { 3, 9 }, { 7, 11 } });
                PrintDupsForEach(new int[,] { { 2, 3, 5 }, { 7, 3, 2 } }, new int[,] { { 3, 9 }, { 7, 11 } });

                string name = "Jerry";
                if (name.Contains("rr"))
                {

                } */
                ReadInNames();
            } finally
            {
                Console.ReadKey();
            }

        }
    }
}
