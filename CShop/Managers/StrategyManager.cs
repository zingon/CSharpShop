using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Managers
{
    class StrategyManager
    {
        private Price.SpringStrategy spring = new Price.SpringStrategy();
        private Price.SummerStrategy summer = new Price.SummerStrategy();
        private Price.FallStrategy fall = new Price.FallStrategy();
        private Price.WinterStrategy winter = new Price.WinterStrategy();

        private Price.BasicStrategy used;
        public StrategyManager()
        {
            DateTime date = DateTime.Now;

            if (date.Month == 12 || (date.Month < 3))
            {
                used = winter;
            }
            else if (date.Month < 12 && date.Month > 8)
            {
                used = fall;
            }
            else if (date.Month < 9 && date.Month > 5)
            {
                used = summer;
            }
            else if (date.Month < 6 && date.Month > 2)
            {
                used = spring;
            }
            //used = spring;
        }

        public void SetCartItems(List<Models.OrderProduct> orderProducts)
        {
            used.OrderProducts = orderProducts;
        }

        public void SetCountOrders(int count)
        {
            summer.CustomerOrders = count;
        }

        public double Total()
        {
            return used.countPrice();
        }
    }
}
