using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Price
{
    class WinterStrategy : BasicStrategy
    {

        public override double countPrice()
        {
            double total = 0.0;
            int sale = 0;
            foreach (Models.OrderProduct product in OrderProducts)
            {
               
                if(product.Name.IndexOf("Léto") > 0 || product.Description.IndexOf("Léto") > 0)
                {
                    sale = 20;
                } else if (product.Name.IndexOf("Jaro") > 0 || product.Description.IndexOf("Jaro") > 0)
                {
                    sale = 10;
                }
                else if (product.Name.IndexOf("Podzim") > 0 || product.Description.IndexOf("Podzim") > 0)
                {
                    sale = 15;
                }
                total += product.Price * product.Count * (1 - (sale / 100));
            }
            return total;
        }
    }
}
