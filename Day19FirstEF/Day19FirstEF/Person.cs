using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19FirstEF
{
    // [Table("MyPerson")]
    public class Person
    {
        // [Key]
        public int Id { get; set; }
        // [Column("TheName")]
        [Index]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return string.Format("Person({0}): {1} is {2} y/o", Id, Name, Age);
        }
    }
}
