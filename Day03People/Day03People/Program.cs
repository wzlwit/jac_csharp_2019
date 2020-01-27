using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03People
{
    class Person
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void Print()
        {
            Console.WriteLine("Person: {0} is {1} y/o", Name, Age);
        }

        /* // these two are equivalent 
        public double Weight { get; set; }

        private double _wegiht2;
        public double Weight2
        {
            get { return _wegiht2;  }
            set { _wegiht2 = value; }
        } */

        /*
        public int Random
        {
            get {
                return new Random().Next(1,100);
            }
        } */

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentOutOfRangeException("Name must be at least 2 characters long");
                }
                _name = value;
            }
        }

        // Age must be between 0-150 (both inclusive)
        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentOutOfRangeException("Age must be between 0-150");
                }
                _age = value;
            }
        }
    }

    class Student : Person
    {
        public Student(string name, int age, string program, double gpa) : base(name, age)
        {
            Program = program;
            GPA = gpa;
        }

        public override void Print()
        {
            Console.WriteLine("Student: {0} is {1} y/o, in {2} with {3} gpa", Name, Age, Program, GPA);
        }

        private string _program;
        public string Program
        {
            get { return _program;  }
            set
            {
                if (value.Length < 2 || value.Length > 100)
                {
                    throw new ArgumentOutOfRangeException("Program name must be 2-100 characters long");
                }
                _program = value;
            }
        }
        private double _gpa;
        public double GPA
        {
            get { return _gpa;  }
            set
            {
                if (value < 0.0 || value > 4.0)
                {
                    throw new ArgumentOutOfRangeException("GPA must be within 0.0-4.0 range");
                }
                _gpa = value;
            }
        }
    }
    /*
    class IPDStudent : Student() {
        public override void Print()
        {
            Console.WriteLine("IPDStudent: {0} is {1} y/o, in {2} with {3} gpa", Name, Age, Program, GPA);
        }
    }*/

    class Teacher : Person
    {
        public Teacher(string name, int age, string field, int yoe) : base(name, age)
        {
            Field = field;
            YOE = yoe;
        }

        private string _field;
        public string Field
        {
            get { return _field;  }
            set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentOutOfRangeException("Field must be at least 2 characters long");
                }
                _field = value;
            }
        }
        private int _yoe;
        public int YOE
        {
            get
            {
                return _yoe;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentOutOfRangeException("Years of experienbce must be between 0-150");
                }
                _yoe = value;
            }
        }

    }

    class Program
    {
        static List<Person> peopleAndOthers = new List<Person>();

        static void Main(string[] args)
        {
            try
            {
                // using an initalizer instead of a constructor
                // Person p = new Person() { Name = "Jerry", Age = 33 };
                Person p = new Person("Jerry", -3);
                // p.Name = "M";
                p.Age = 77;
                Console.WriteLine("Person: {0} is {1} y/o", p.Name, p.Age);
            } catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                Console.WriteLine(ex.StackTrace);
            } finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
