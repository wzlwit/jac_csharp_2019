using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20CarsAndOwnersEF
{
    public class Owner
    {
        public Owner()
        {
            CarsCollection = new HashSet<Car>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } // up to 100 characters

        public /* virtual */ ICollection<Car> CarsCollection { get; set; } // relation
    }
}
