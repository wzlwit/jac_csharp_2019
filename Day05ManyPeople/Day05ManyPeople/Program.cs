using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05ManyPeople
{
    class Person
    {
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

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

        public override string ToString()
        {
            return string.Format("Person: {0} is {1} y/o", Name, Age);
        }
    }

    class Student : Person
    {
        public Student(string name, int age, string program, double gpa) : base(name, age)
        {
            Program = program;
            GPA = gpa;
        }

        private string _program;
        public string Program
        {
            get { return _program; }
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
            get { return _gpa; }
            set
            {
                if (value < 0.0 || value > 4.0)
                {
                    throw new ArgumentOutOfRangeException("GPA must be within 0.0-4.0 range");
                }
                _gpa = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Student: {0} is {1} y/o in {2} with gpa {3}", Name, Age, Program, GPA);
        }

    }

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
            get { return _field; }
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

        public override String ToString()
        {
            return string.Format("Teacher: {0} is {1} y/o teaching {2} since {3} years", Name, Age, Field, YOE);
        }
    }
    
    class Program
    {
        const string FILE_NAME = @"..\..\..\people.txt";

        static List<Person> people = new List<Person>();

        static void Main(string[] args)
        {
            try
            {
                string[] linesArray = File.ReadAllLines(FILE_NAME);
                foreach (string line in linesArray)
                {
                    try
                    {
                        string[] data = line.Split(';');
                        switch (data[0])
                        {
                            case "Person":
                                {
                                    if (data.Length != 3)
                                    {
                                        Console.WriteLine("Error, skipping line - Invalid field count in line: " + line);
                                        continue;
                                    }
                                    string name = data[1];
                                    string ageStr = data[2];
                                    int age;
                                    if (!int.TryParse(ageStr, out age))
                                    {
                                        Console.WriteLine("Error, skipping line - Age must be an integer in line: " + line);
                                        continue;
                                    }
                                    Person p = new Person(name, age);
                                    people.Add(p);
                                }
                                break;
                            case "Student":
                                {
                                    if (data.Length != 5)
                                    {
                                        Console.WriteLine("Error, skipping line - Invalid field count in line: " + line);
                                        continue;
                                    }
                                    string name = data[1];
                                    string ageStr = data[2];
                                    int age;
                                    if (!int.TryParse(ageStr, out age))
                                    {
                                        Console.WriteLine("Error, skipping line - Age must be an integer in line: " + line);
                                        continue;
                                    }
                                    string program = data[3];
                                    string gpaStr = data[4];
                                    double gpa;
                                    if (!double.TryParse(gpaStr, out gpa))
                                    {
                                        Console.WriteLine("Error, skipping line - Gpa must be numerical in line: " + line);
                                        continue;
                                    }
                                    people.Add(new Student(name, age, program, gpa));
                                }
                                break;
                            case "Teacher":
                                {
                                    if (data.Length != 5)
                                    {
                                        Console.WriteLine("Error, skipping line - Invalid field count in line: " + line);
                                        continue;
                                    }
                                    string name = data[1];
                                    string ageStr = data[2];
                                    int age;
                                    if (!int.TryParse(ageStr, out age))
                                    {
                                        Console.WriteLine("Error, skipping line - Age must be an integer in line: " + line);
                                        continue;
                                    }
                                    string field = data[3];
                                    string yoeStr = data[4];
                                    int yoe;
                                    if (!int.TryParse(yoeStr, out yoe))
                                    {
                                        Console.WriteLine("Error, skipping line - Yoe must be an integer in line: " + line);
                                        continue;
                                    }
                                    people.Add(new Teacher(name, age, field, yoe));
                                }
                                break;
                            default:
                                // FIXME: handle invalid type
                                Console.WriteLine("Error, skipping line. Don't know how to make {0} in line: {1}", data[0], line);
                                break;
                        }
                    } catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("Error, skipping line {0} in line: {1}", ex.Message, line);
                    }
                }
                // PARSING DONE
                foreach (Person p in people)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine("\n==============ONLY STUDENTS:");
                foreach (Person p in people)
                {
                    if (p is Student)
                    {
                        Console.WriteLine(p);
                    }
                }
                Console.WriteLine("\n==============ONLY TEACHERS:");
                foreach (Person p in people)
                {
                    if (p is Teacher)
                    {
                        Teacher t = p as Teacher;
                        if (t.YOE > 30)
                        {
                            Console.Write("HIGHLY EXPERIENCED ");
                        }
                        Console.WriteLine(p);
                    }
                }
                Console.WriteLine("\n==============ONLY PERSONS:");
                foreach (Person p in people)
                {
                    // BAD IDEA: if (p is Person && !(p is Student) && !(p is Teacher) )
                    if (p.GetType() == typeof(Person))
                    {
                        Console.WriteLine(p);
                    }
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error accessing file: " + ex.Message);
            }
            finally
            {
                Console.Write("Press any key to end");
                Console.ReadKey();
            }
        }
    }
}
