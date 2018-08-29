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
        private Cart cart;

        public Order(SQLiteConnection db, Cart cart)
        {
            this.db = db;
            this.cart = cart;
        }

        public void create(IModel model)
        {
            Models.Order ord = (Models.Order)model;
            if(ord.Customer.Id == 0)
            {
                ord.Customer = createCustomer(ord.Customer);
            }
            using (var command = new SQLiteCommand { Connection = this.db })
            {
                try
                {
                    command.CommandText = (@"INSERT INTO orders (customer_id,created) VALUES (@cid,@creat)");
                    command.Parameters.AddWithValue("@cid", ord.Customer.Id);
                    command.Parameters.AddWithValue("@creat", ord.Created.ToString());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                   
                    command.CommandText = "SELECT last_insert_rowid()";
                    long orderId = (long)command.ExecuteScalar();

                    foreach(Models.OrderProduct product in this.cart.GetCartItems())
                    {
                        command.CommandText = (@"INSERT INTO order_product (order_id,product_id,price,count) VALUES (@oid,@pprod,@pric, @cou)");
                        command.Parameters.AddWithValue("@oid", orderId);
                        command.Parameters.AddWithValue("@pprod", product.Id);
                        command.Parameters.AddWithValue("@pric", product.Price);
                        command.Parameters.AddWithValue("@cou", product.Count);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    this.cart.ResetCart();

                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }


        }

        public Models.Customer createCustomer(Models.Customer model)
        {   
            string sql = "INSERT INTO customer (name,address) VALUES (@name ,@address)";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@name", model.Name);
            command.Parameters.AddWithValue("@address", model.Address);
            command.ExecuteNonQuery();

             sql = "SELECT id FROM customer WHERE name = @name LIMIT 1";
            SQLiteCommand command1 = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@name", model.Name);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                model.Id = reader.GetInt32(0);
            }
            
            

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
            string sql = "SELECT COUNT(*) FROM orders WHERE customer_id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", customer_id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public List<Models.Order> getAll()
        {
            string sql = "SELECT * FROM orders";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Models.Order> orders = new List<Models.Order>();
            
            while (reader.Read())
            {
                orders.Add(buildModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
            }
            return orders;
        }
       
        public IModel findBy(int id)
        {
            throw new NotImplementedException();
        }
        public IModel findCustomerBy(int id)
        {
            string sql = "SELECT * FROM customer WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, this.db);
            command.Parameters.AddWithValue("@id", id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return this.buildCustomerModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        }

        public void remove(IModel model)
        {
            throw new NotImplementedException();
        }

        public void update(IModel model)
        {
            Models.Order ord = (Models.Order)model;

            if (ord.Customer.Id == 0)
            {
                ord.Customer = createCustomer(ord.Customer);
            }
            using (var command = new SQLiteCommand { Connection = this.db })
            {
                try
                {
                    command.CommandText = (@"UPDATE orders SET customer_id = @cid, created = @creat, state = @stat WHERE id = @id");
                    command.Parameters.AddWithValue("@id", ord.Id);
                    command.Parameters.AddWithValue("@cid", ord.Customer.Id);
                    command.Parameters.AddWithValue("@creat", ord.Created.ToString());
                    command.Parameters.AddWithValue("@stat", ord.State);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
        }

        internal Models.Order buildModel(int id, int customer_id, string created, string state)
        {
            Models.Order model = new Models.Order();
            model.Id = id;
            model.State = state;
            model.Created = DateTime.Parse(created);
            
            Container container = Container.Instance;
           
            model.Customer = this.findCustomerBy(customer_id) as Models.Customer;

            return model;
        }
        internal Models.Customer buildCustomerModel(int id, string name, string address)
        {
            Models.Customer model = new Models.Customer();
            model.Id = id;
            model.Name = name;
            model.Address = address;
            return model;
        }
    }
}
