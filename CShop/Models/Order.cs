using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Models
{
    class Order : IModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime Created { get; set; }
        public List<Product> Products { get; set; }
    }
}
