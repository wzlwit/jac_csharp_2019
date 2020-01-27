using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04PeopleFromFile
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


    class Program
    {
        const string FILE_PATH = @"..\..\..\people.txt";

        static List<Person> people = new List<Person>();

        static void Main(string[] args)
        {
            try
            {

                string[] lines = File.ReadAllLines(FILE_PATH);
                foreach (string l in lines)
                {
                    try
                    {
                        string[] data = l.Split(';');
                        if (data.Length != 2)
                        {
                            throw new InvalidDataException("Line must have exactly two data items: " + l);
                        }
                        string name = data[0];
                        int age = int.Parse(data[1]);
                        Person p = new Person(name, age);
                        people.Add(p);
                    }
                    catch (Exception ex)
                    {
                        if (ex is FormatException || ex is OverflowException || ex is ArgumentOutOfRangeException || ex is InvalidDataException) {
                            Console.WriteLine("Error occured: " + ex.Message);
                        } else
                        {
                            throw ex;
                        }
                    }
                }
                //
                foreach (Object o in people)
                {
                    // Console.WriteLine("Person: {0} is {1} y/o", p.Name, p.Age);
                    Console.WriteLine(o);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("press a key to end");
                Console.ReadKey();
            }
        }
    }
}
