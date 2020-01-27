using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20CarsAndOwnersEF
{
    class ParkingContext : DbContext
    {
        public ParkingContext() : base() { } // TODO: explain file name
        virtual public DbSet<Owner> Owners { get; set; }
        virtual public DbSet<Car> Cars { get; set; }
    }
}
