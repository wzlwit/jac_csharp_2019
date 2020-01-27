using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Static
{
    class Box
    {
        public Box()
        {
            Counter++;
            Index = Counter;
            GetCount();
        }

        private static int Counter;
        public int GetCount() {
            // Number++;
            return Counter;
        }

        public int Index;
        public string Contents;
        public int Number;
    }


    class Program
    {
        static void Main(string[] args)
        {
            Box b1 = new Box() { Contents = "Jerry" };
            Box b2 = new Box() { Contents = "Maria" };
            Console.WriteLine("Counter is " + b1.GetCount());
            Console.ReadKey();
        }
    }
}
