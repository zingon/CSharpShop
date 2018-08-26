using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CShop.Models;

namespace CShop.Price
{
    public abstract class BasicStrategy : IStrategy
    {
   

        internal List<Models.OrderProduct> OrderProducts { get; set; }

        public abstract double countPrice();

        protected double basicTotalPrice()
        {
            double total = 0.0;
            foreach(Models.OrderProduct product in OrderProducts)
            {
                total += product.Price * product.Count;
            }
            return total;
        }
    }
}
