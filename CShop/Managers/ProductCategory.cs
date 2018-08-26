using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CShop.Models;
using System.Data.SQLite;

namespace CShop.Managers
{
    class ProductCategory : IManager
    {
        private bool tiny = true;
        private SQLiteConnection db;

        public ProductCategory(SQLiteConnection db)
        {
            this.db = db;
        }

        public bool Tiny { get => tiny; set => tiny = value; }

        public void create(IModel model)
        {
            Models.ProductCategory cat = (Models.ProductCategory)model;
            string sql = "INSERT INTO product_category (name) VALUES (@name)";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@name", cat.Name);
            command.ExecuteNonQuery();
        }

        public IModel findBy(int id)
        {
            string sql = "SELECT * FROM product_category WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return buildModel(reader.GetInt32(0), reader.GetString(1));
        }

        public List<Models.ProductCategory> getAll()
        {
            string sql = "SELECT * FROM product_category";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            SQLiteDataReader reader = command.ExecuteReader();

            
            List<Models.ProductCategory> categories = new List<Models.ProductCategory>();
            while (reader.Read())
            {
                Models.ProductCategory model = this.buildModel(reader.GetInt32(0), reader.GetString(1));
                Container container = Container.Instance;
                Managers.Product productManager = container.Get<Managers.Product>("manager.product");

                model.Products = productManager.GetByCategory(model.Id);
                categories.Add(model);
                this.Tiny = true;
            }
            return categories;
        }

        public void remove(IModel model)
        {
            Models.ProductCategory cat = (Models.ProductCategory)model;
            string sql = "DELETE FROM product_category WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", cat.Id);
            command.ExecuteNonQuery();
        }

        public void update(IModel model)
        {
            Models.ProductCategory cat = (Models.ProductCategory)model;
            string sql = "UPDATE product_category SET name=@name WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", cat.Id);
            command.Parameters.AddWithValue("@name", cat.Name);
            command.ExecuteNonQuery();
        }

        internal Models.ProductCategory buildModel(int id, string name)
        {
            Models.ProductCategory model = new Models.ProductCategory();
            model.Id = id;
            model.Name = name;
           
            return model;
        }
    }
}
