using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21Indexer
{
    public class MagicalNumbersStorage
    {
        public int this[int idxOne, int idxTwo]
        {
            get
            {
                return idxOne * idxTwo;
            }
            set
            {

            }
        }

        public int this[int index]
        {
            get
            {
                return index * 2 + 2;
            }
            set
            {
                Console.WriteLine(String.Format("No such operation, index={0}, value={1}", index, value));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MagicalNumbersStorage mns = new MagicalNumbersStorage();
                mns[5] = 22;
                Console.WriteLine("returned: " + mns[55]);
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
