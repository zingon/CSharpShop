using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Price
{
    class SummerStrategy : BasicStrategy
    {
        public int CustomerOrders { get; set; }

        public override double countPrice()
        {
            double total = 0.0;
           

            foreach (Models.OrderProduct product in OrderProducts)
            {
                total += product.Price * product.Count;
            }

            int sale = 0;
            if (CustomerOrders >= 5)
            {
                sale = 10;
            }
            else if (CustomerOrders >= 10)
            {
                sale = 20;
            }

            return total * (1 - (sale/100));
        }
    }
}
