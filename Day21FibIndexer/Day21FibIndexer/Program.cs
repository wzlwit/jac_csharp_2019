using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21FibIndexer
{
    class FibStore
    {

        public long this[int n]
        {
            get
            {
                if (n < 1) throw new IndexOutOfRangeException("Fibs only exist for positive numbers");
                return getFib(n);
            }
        }

        private long getFib(int n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            return getFib(n - 1) + getFib(n - 2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FibStore fs = new FibStore();
                Console.WriteLine(fs[1]);
                Console.WriteLine(fs[14]);
                Console.WriteLine(fs[8]);
                Console.WriteLine(fs[-4]); // throws IllegalArgumentException or similar
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
