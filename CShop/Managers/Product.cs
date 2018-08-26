using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using CShop.Models;

namespace CShop.Managers
{
    class Product : IManager
    {
        private SQLiteConnection db;

        public Product(SQLiteConnection db)
        {
            this.db = db;
        }

        public void create(IModel model)
        {
            Models.Product prod = (Models.Product)model;
            string sql = "INSERT INTO product (name,price,product_category_id,description) VALUES (@name,@price,@cat_id,@desc)";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@name", prod.Name);
            command.Parameters.AddWithValue("@price", prod.Price);
            command.Parameters.AddWithValue("@cat_id", prod.Category.Id);
            command.Parameters.AddWithValue("@desc", prod.Description);
            command.ExecuteNonQuery();
        }

        public IModel findBy(int id)
        {
            string sql = "SELECT * FROM product WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return buildModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetDouble(2), reader.GetString(4));
        }

        public void remove(IModel model)
        {
            Models.Product prod = (Models.Product)model;
            string sql = "DELETE FROM product WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", prod.Id);
            command.ExecuteNonQuery();
        }

        public void update(IModel model)
        {
            Models.Product prod = (Models.Product)model;
            string sql = "UPDATE product SET name = @name , description = @desc, product_category_id = @cat_id, price = @price WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", prod.Id);
            command.Parameters.AddWithValue("@name", prod.Name);
            command.Parameters.AddWithValue("@desc", prod.Description);
            command.Parameters.AddWithValue("@cat_id", prod.Category.Id);
            command.Parameters.AddWithValue("@price", prod.Price);
            command.ExecuteNonQuery();
        }


        public List<Models.Product> getAll()
        {
            string sql = "SELECT * FROM product";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Models.Product> products = new List<Models.Product>();
            while (reader.Read())
            {
                products.Add(buildModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetDouble(2), reader.GetString(4)));
            }
            return products;
        }

        public List<Models.Product> GetByCategory(int category_id)
        {
            string sql = "SELECT * FROM product WHERE product_category_id = @cat_id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@cat_id", category_id);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Models.Product> products = new List<Models.Product>();
            while (reader.Read())
            {
                products.Add(buildModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), reader.GetDouble(2), reader.GetString(4)));
            }
            return products;
        }
        internal Models.Product buildModel(int id, string name,int cat_id, double price, string desc)
        {
            Models.Product model = new Models.Product();
            model.Id = id;
            model.Name = name;
            model.Description = desc;
            model.Price = price;
            Container container = Container.Instance;
            Managers.ProductCategory category = container.Get<Managers.ProductCategory>("manager.productCategory");
            model.Category = category.findBy(cat_id) as Models.ProductCategory; 

            return model;
        }
    }
}
