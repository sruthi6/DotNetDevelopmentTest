using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery
{
    public class Customer
    {
        public string CustomerType { get; set; }

        public int ArrivalTime { get; set; }

        public int Items { get; set; }

        public int ElapsedTimeToCompleteThisCustomer { get; set; }
    }
}
