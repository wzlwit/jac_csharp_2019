using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05Virtuals
{
    class A
    {
        public virtual void Foo() { Console.WriteLine("A::Foo()"); }
    }

    class B : A
    {
        public override void Foo() { Console.WriteLine("B::Foo()"); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            a.Foo();
            b.Foo();
            A reallyB = b; // down-cast
            reallyB.Foo();

            Console.ReadKey();
        }
    }
}
