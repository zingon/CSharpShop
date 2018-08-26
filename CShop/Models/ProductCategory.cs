using System.Collections.Generic;

namespace CShop.Models
{
    public class ProductCategory : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Product> Products { get; set;}
    }
}