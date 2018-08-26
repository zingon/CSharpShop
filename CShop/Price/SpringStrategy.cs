using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Price
{
    public class SpringStrategy : BasicStrategy
    {
        private int specialSale = 0;
        public override double countPrice()
        {
            double total = 0.0;
            int specialId = this.FindSpecial();

            foreach (Models.OrderProduct product in OrderProducts)
            {
                if(specialId > 0 && specialId != product.Id)
                {
                    total += (product.Price * product.Count * (1 - (specialSale / 100)));
                } else
                {
                    total += product.Price * product.Count;
                }
                
            }
            return total;
        }

        public int FindSpecial()
        {
            int id = 0;
            foreach (Models.OrderProduct product in OrderProducts)
            {
                if(product.Count >= 3 && OrderProducts.Count > 3 && specialSale < 20)
                {
                    specialSale = 20;
                    id = product.Id;
                } else if (product.Count >= 8 && OrderProducts.Count > 3 && specialSale < 30)
                {
                    specialSale = 30;
                    id = product.Id;
                }
                else if (product.Count >= 12 && OrderProducts.Count > 3)
                {
                    specialSale = 40;
                    id = product.Id;
                }
            }
            return id;
        }
    }
}
