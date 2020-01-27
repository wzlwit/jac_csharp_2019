using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16TodosDB
{
    public class Todo
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime DueDate { get; set; }
        public ETaskStatus IsDone { get; set; }
    }

    public enum ETaskStatus { Pending = 2, Done = 1}

}
