using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLWD6.CustomORM.Attributes
{
    public class Identity : Attribute
    {
        public int Seed { get; set; }
        public int Increment { get; set; }
        public Identity(int seed, int increment)
        {
            Seed = seed;
            Increment = increment;
        }
    }
}
