using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using CShop.Managers;

namespace CShop
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Container container = Container.Instance;
              
            SQLiteConnection db = new SQLiteConnection("Data Source=../../database.sqlite;Version=3;");
            db.Open();
            container.Add<SQLiteConnection>("db", db);
            container.Add<Product>("manager.product", new Product(db));
            container.Add<ProductCategory>("manager.productCategory", new ProductCategory(db));
            container.Add<Cart>("manager.cart", new Cart());
            container.Add<StrategyManager>("manager.strategy", new StrategyManager());
            container.Add<Order>("manager.order", new Order(db));


        }
    }
}
