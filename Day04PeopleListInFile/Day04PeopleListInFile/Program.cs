using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04PeopleListInFile
{
    class InvalidDataException : Exception
    {
        public InvalidDataException() { }
        public InvalidDataException(string msg) : base(msg) { }
        public InvalidDataException(string msg, Exception inner) : base(msg, inner) { }
    }

    class Person
    {

        public Person(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length < 2 || value.Length > 100)
                {
                    throw new InvalidDataException("Name must be 2-100 characters long");
                }
                _name = value;
            }
        }

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

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (value.Length < 2 || value.Length > 100)
                {
                    throw new ArgumentOutOfRangeException("City must be 2-100 characters long");
                }
                _city = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} is {1} from {2}", Name, Age, City);
        }

    }

    class Program
    {

        const string FILE_PATH = @"..\..\..\people.txt";

        static List<Person> people = new List<Person>();

        static void AddPersonInfo()
        {
            try
            {
                Console.WriteLine("Adding a person.");
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                Console.Write("Enter age: ");
                string ageStr = Console.ReadLine();
                int age;
                /*
                if (!int.TryParse(ageStr, out age))
                {
                    Console.WriteLine("Error: Age must be an integer");
                    return;
                }*/
                age = int.Parse(ageStr);
                Console.Write("Enter city: ");
                string city = Console.ReadLine();

                Person p = new Person(name, age, city);
                people.Add(p);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: Age must be an integer");
            }
        }

        static void ListAllPersonsInfo()
        {
            Console.WriteLine("Listing all persons");
            foreach (Person p in people)
            {
                Console.WriteLine(p);
            }
        }

        static void FindPersonByName()
        {
            Console.WriteLine("Enter partial person name:");
            string partial = Console.ReadLine();
            Console.WriteLine("Matches found:");
            foreach (Person p in people)
            {
                if (p.Name.Contains(partial))
                {
                    Console.WriteLine(p);
                }
            }
        }

        static void FindPersonYoungerThan()
        {

        }

        static void ReadAllPeopleFromFile()
        {
            try
            {
                if (!File.Exists(FILE_PATH))
                {
                    return;
                }
                string[] contents = File.ReadAllLines(FILE_PATH);
                foreach (string line in contents)
                {
                    string[] data = line.Split(';');
                    if (data.Length != 3)
                    {
                        Console.WriteLine("Line corrupted, must have 3 fields exactly: " + line);
                        continue;
                    }
                    int age;
                    if (!int.TryParse(data[1], out age))
                    {
                        Console.WriteLine("Line corrupted, age must be integer: " + line);
                        continue;
                    }
                    try
                    {
                        Person p = new Person(data[0], age, data[2]);
                        people.Add(p);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("Line corrupted: " + line + "\nError:" + ex.Message);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: failed to read data from file: " + ex.Message);
            }
        }

        static void SaveAllPeopleToFile()
        {
            try
            {
                List<string> contents = new List<string>();
                foreach (Person p in people)
                {
                    contents.Add(String.Format("{0};{1};{2}", p.Name, p.Age, p.City));
                }
                File.WriteAllLines(FILE_PATH, contents);
            } catch (IOException ex)
            {
                Console.WriteLine("Error: failed to save data to file: " + ex.Message);
            }
        }

        static int GetUserMenuChoice()
        {
            while (true)
            {
                Console.WriteLine(
                    "What do you want to do?\n" +
                    "1.Add person info\n" +
                    "2.List persons info\n" +
                    "3.Find and list a person by name\n" +
                    "4.Find and list persons younger than age\n" +
                    "0.Exit\n" +
                    "Choice: ");
                string line = Console.ReadLine();
                int choice;
                if (int.TryParse(line, out choice))
                {
                    if (choice >= 0 && choice <= 4)
                    {
                        Console.WriteLine();
                        return choice;
                    }
                }
                Console.WriteLine("Invalid choice\n");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                ReadAllPeopleFromFile();
                int choice;
                do
                {
                    choice = GetUserMenuChoice();
                    switch (choice)
                    {
                        case 1:
                            AddPersonInfo();
                            break;
                        case 2:
                            ListAllPersonsInfo();
                            break;
                        case 3:
                            FindPersonByName();
                            break;
                        case 4:
                            FindPersonYoungerThan();
                            break;
                        case 0:
                            Console.WriteLine("Good bye.");
                            break;
                        default:
                            // yes, you must have a default handler - always
                            Console.WriteLine("Internal error: unknown choice.");
                            break;
                    }
                    Console.WriteLine();
                } while (choice != 0);
                SaveAllPeopleToFile();
            }
            finally
            {
                Console.WriteLine("press a key to end");
                Console.ReadKey();
            }
        }
    }
}
