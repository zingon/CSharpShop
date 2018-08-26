using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CShop.Models;

namespace CShop.Managers
{
    class Cart
    {
        private List<KeyValuePair<int, OrderProduct>> items;

        public Cart()
        {
            this.items = new List<KeyValuePair<int, OrderProduct>>();
        }

        internal List<KeyValuePair<int, OrderProduct>> Items { get => items; set => items = value; }

        public List<OrderProduct> GetCartItems()
        {
            List<OrderProduct> result = new List<OrderProduct>();
            foreach (KeyValuePair<int, OrderProduct> item in items)
            {
                result.Add(item.Value);
            }
            return result;
        }
        public void ResetCart()
        {
            this.items = new List<KeyValuePair<int, OrderProduct>>();
        }
        private object findItem(OrderProduct product)
        {
            foreach (KeyValuePair<int, OrderProduct> item in items)
            {
               if(item.Key == product.Id)
                {
                    return item;
                }
            }
            return null;
        }
        public void AddToCart(Models.OrderProduct product)
        {
            if(findItem(product) == null ) {
                items.Add(new KeyValuePair<int, OrderProduct>(product.Id, product));
            } else
            {
                 KeyValuePair<int, OrderProduct> orderProduct = (KeyValuePair<int, OrderProduct>)findItem(product);
                orderProduct.Value.Count += product.Count;
                Console.WriteLine(orderProduct.Value.Count);
            }
            
        }

        
    }
}
