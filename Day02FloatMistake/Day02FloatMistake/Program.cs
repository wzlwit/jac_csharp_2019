using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02FloatMistake
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal val = 1.0m;
            for (int i=0; i < 10; i++)
            {
                val -= 0.1m;
            }
            Console.WriteLine(val);
            Console.ReadKey();
        }
    }
}
