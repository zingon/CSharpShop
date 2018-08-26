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
            throw new NotImplementedException();
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
