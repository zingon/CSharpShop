using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Price
{
    class FallStrategy : BasicStrategy
    {

        public override double countPrice()
        {
            double total = 0.0;
            int sale = 0;
            foreach (Models.OrderProduct product in OrderProducts)
            {
                if(product.Id == 2 || product.Id == 20)
                {
                    sale = 20;
                } else if(product.Id == 4 ||product.Id == 5 || product.Id == 7)
                {
                    sale = 10;
                } else if (product.Id < 50 && product.Id > 20)
                {
                    sale = 5;
                }
                total += product.Price * product.Count * (1- (sale/100));
            }
            return total;
        }
    }
}
