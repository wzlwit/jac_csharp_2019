using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20CarsAndOwnersEF
{
    class Program
    {
        static ParkingContext ctx;

        static int GetMenuChoice()
        {
            while (true)
            {
                Console.WriteLine(
    @"1. List all cars and their owner
2. List all owners and their cars
3. Add a car (no owner)
4. Add an owner (no cars)
5. Assign car to a new owner (or no owner)
6. Delete an owner with all cars they own
0. Quit");
            int choice = int.Parse(Console.ReadLine()); // fixme
                if (choice >= 0 && choice <= 6 )
                {
                    return choice;
                }
            }
        }

        static void Main(string[] args)
        {
            ctx = new ParkingContext();

            int choice = 0;
            do
            {
                try
                {
                    choice = GetMenuChoice();
                    switch (choice)
                    {
                        case 1:
                            ListAllCarsAndTheirOwners();
                            break;
                        case 2:
                            ListAllOwnersAndTheirCars();
                            break;
                        case 3:
                            AddCar();
                            break;
                        case 4:
                            AddOwner();
                            break;
                        case 5:
                            AssignCarToNewOwner();
                            break;
                        case 6:
                            DeleteOwnerAndAllCarsTheyOwn();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Internal error: unknown menu choice");
                            break;
                    }
                    Console.WriteLine();
                } catch (DataException ex)
                {
                    Console.WriteLine("DataException: " + ex.Message);
                }
            } while (choice != 0);
        }

        private static void DeleteOwnerAndAllCarsTheyOwn()
        {
            throw new NotImplementedException();
        }

        private static void AssignCarToNewOwner()
        {
            List<Car> carsList = ctx.Cars.ToList();
            foreach (Car c in carsList)
            {
                string ownerInfo = c.Owner == null ? "nobody" : c.Owner.Id + "-" + c.Owner.Name;
                Console.WriteLine("Car({0}): {1} made in {2} owned by {3}",
                    c.Id, c.MakeModel, c.YearOfProd, ownerInfo);
            }
            // var list = from o in ctx.Owners select o;
            var ownersList = ctx.Owners.Include("CarsCollection").ToList();
            foreach (Owner o in ownersList)
            {
                Console.WriteLine("Owner({0}): {1} has {2} cars", o.Id, o.Name, o.CarsCollection.Count);
            }
            Console.Write("Enter car Id to assign: ");
            int carId = int.Parse(Console.ReadLine());
            Console.Write("Enter owner Id to assign (0 to remove owner): "); // TODO: handle 0
            int ownerId = int.Parse(Console.ReadLine());
            //
            Car selCar = ctx.Cars.Where(car => car.Id == carId).SingleOrDefault();
            Owner selOwner = ctx.Owners.Where(owner => owner.Id == ownerId).SingleOrDefault();
            if (selCar == null || selOwner == null)
            {
                Console.WriteLine("Car or Owner with specified Id not found");
                return;
            }
            selCar.Owner = selOwner; // just update one direction for now, see if it works
            ctx.SaveChanges();
            Console.WriteLine("Record updated");
        }

        private static void AddOwner()
        {
            Console.Write("Enter owner name: ");
            string name = Console.ReadLine();
            Owner o = new Owner() { Name = name };
            ctx.Owners.Add(o);
            ctx.SaveChanges();
            Console.WriteLine("Record created");
        }

        private static void AddCar()
        {
            Console.Write("Enter car make/model: ");
            string makeModel = Console.ReadLine();
            Console.Write("Enter car production year: ");
            int yop = int.Parse(Console.ReadLine());
            Car c = new Car() { MakeModel = makeModel, YearOfProd = yop };
            ctx.Cars.Add(c);
            ctx.SaveChanges();
            Console.WriteLine("Record created");
        }

        private static void ListAllOwnersAndTheirCars()
        {
            // var list = from o in ctx.Owners select o;
            var list = ctx.Owners.Include("CarsCollection").ToList();
            foreach (Owner o in list)
            {
                Console.WriteLine("Owner({0}): {1} has {2} cars", o.Id, o.Name, o.CarsCollection.Count);
                foreach (Car c in o.CarsCollection) {
                    Console.WriteLine("    Car({0}): {1} made in {2}",
                   c.Id, c.MakeModel, c.YearOfProd);
                }
            }
        }

        private static void ListAllCarsAndTheirOwners()
        {
            throw new NotImplementedException();
        }
    }
}
