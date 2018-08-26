using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.Models
{
    public abstract class AProduct :IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Models.ProductCategory Category { get; set; }
        public string Description { get; set; }

    }
}
