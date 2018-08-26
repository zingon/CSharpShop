using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CShop.Models;

namespace CShop.Managers
{
    class Order :IManager
    {
        private SQLiteConnection db;

        public Order(SQLiteConnection db)
        {
            this.db = db;
        }

        public void create(IModel model)
        {
            Models.Order ord = (Models.Order)model;
            if(ord.Customer.Id == 0)
            {
                ord.Customer = createCustomer(ord.Customer);
            }
            string sql = "INSERT INTO order (customer_id,created) VALUES (@customer_id,@created)";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@customer_id", ord.Customer.Id);
            command.Parameters.AddWithValue("@created", ord.Created.ToString());
            command.ExecuteNonQuery();


        }

        public Models.Customer createCustomer(Models.Customer model)
        {   
            string sql = "INSERT INTO customer (name,address) VALUES (@name,@address)";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@name", model.Name);
            command.Parameters.AddWithValue("@address", model.Address);
            command.ExecuteNonQuery();

             sql = "SELECT id FROM customer WHERE name = @name";
            SQLiteCommand command1 = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@name", model.Name);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            model.Id = reader.GetInt32(0);

            return model;
        }
        public List<Customer> GetAllCustomers()
        {
            string sql = "SELECT * FROM customer";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            SQLiteDataReader reader = command.ExecuteReader();


            List<Models.Customer> customers = new List<Models.Customer>();
            while (reader.Read())
            {
                Models.Customer model = new Customer();
                model.Id = reader.GetInt32(0);
                model.Name = reader.GetString(1);
                model.Address = reader.GetString(2);
                customers.Add(model);
            }
            return customers;
        }
        public int CountCustomerOrders(int customer_id)
        {
            string sql = "SELECT COUNT(*) FROM order WHERE customer = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", customer_id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public IModel findBy(int id)
        {
            throw new NotImplementedException();
        }

        public void remove(IModel model)
        {
            throw new NotImplementedException();
        }

        public void update(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
