using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19FirstEF
{
    public class FirstContext : DbContext
    {
        public FirstContext() : base() { }
        virtual public DbSet<Person> People { get; set; }
    }
}
