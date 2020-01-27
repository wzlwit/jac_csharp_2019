using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19FirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var ctx = new FirstContext())
                {
                    // Equivalent of INSERT
                    Person p1 = new Person() { Name = "marry", Age = new Random().Next(1, 99) };
                    ctx.People.Add(p1);
                    ctx.SaveChanges();
                }
                // update
                using (var ctx = new FirstContext())
                {
                    // this way it may result in ArrayIndexOutOfBounds Exception
                    var personList = (from p in ctx.People where p.Id == 2 select p).ToList();
                    Person person = (personList.Count() == 0) ? null : personList[0];

                    if (person != null)
                    {
                        Console.WriteLine("Person to update found: " + person);
                        person.Age = 99;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("record to update not found");
                    }
                }

                using (var ctx = new FirstContext())
                {
                    // this way it may result in ArrayIndexOutOfBounds Exception
                    var personList = (from p in ctx.People where p.Id == 4 select p).ToList();
                    Person person = personList.Count() == 0 ? null : personList[0];
                    if (person != null)
                    {
                        Console.WriteLine("Person to delete found: " + person);
                        ctx.People.Remove(person);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("record to delete not found");
                    }
                }


                // select * resulting in attached/tracked entities
                using (var ctx = new FirstContext())
                {
                    var people = from p in ctx.People select p;
                    foreach (var p in people)
                    {
                        Console.WriteLine(p);
                    }
                }
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }

        }
    }
}
