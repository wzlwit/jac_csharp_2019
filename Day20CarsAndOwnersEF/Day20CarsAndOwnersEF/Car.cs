using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20CarsAndOwnersEF
{
    public class Car
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string MakeModel { get; set; } // up to 100 characters

        public int YearOfProd { get; set; }

        // public int OwnerId { get; set; }
        public /* virtual */ Owner Owner { get; set; } // relation, make nullable
    }
}
