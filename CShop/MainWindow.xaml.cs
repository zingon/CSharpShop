using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CShop.Models;

namespace CShop
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.ProductCategory selectedCategory;
        private Managers.Product productManager;
        private Managers.ProductCategory productCategoryManager;
        private Managers.Cart cartManager;
        private Managers.StrategyManager strategyManager;
        private Managers.Order orderManager;
        public MainWindow()
        {
            InitializeComponent();
            Container container = Container.Instance;
            this.productManager = container.Get<Managers.Product>("manager.product");
            this.cartManager = container.Get<Managers.Cart>("manager.cart");
            this.productCategoryManager = container.Get<Managers.ProductCategory>("manager.productCategory");
            this.strategyManager = container.Get<Managers.StrategyManager>("manager.strategy");
            this.orderManager = container.Get<Managers.Order>("manager.order");
            CategoriesDatagrid.ItemsSource = productCategoryManager.getAll();
            CartDatagrid.ItemsSource = cartManager.GetCartItems();
        }
        internal List<Product> Details()
        {
            Console.WriteLine(selectedCategory.Name);
            return this.SelectedCategory.Products as List<Product>;
        }
        public ProductCategory SelectedCategory { get => selectedCategory; set => selectedCategory = value; }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            Models.Product model = (sender as Button).DataContext as Models.Product;
            Models.OrderProduct ordered = new OrderProduct();
            ordered.Name = model.Name;
            ordered.Id = model.Id;
            ordered.Price = model.Price;
            ordered.Category = model.Category;
            ordered.Count = 1;
           
            cartManager.AddToCart(ordered);
            CartDatagrid.ItemsSource = cartManager.GetCartItems();
            Recount();
        }
        private void RecountClick(object sender, RoutedEventArgs e)
        {
            Recount();
        }
        private void Recount()
        {
            Models.Customer customer = (Models.Customer)CustomersBox.SelectedItem;
            List<Models.OrderProduct> ordered = this.cartManager.GetCartItems();
            this.strategyManager.SetCartItems(ordered);
            this.strategyManager.SetCountOrders(this.orderManager.CountCustomerOrders(customer.Id));
            Console.WriteLine(this.strategyManager.Total());
            this.totalPrice.Text = Convert.ToString(this.strategyManager.Total()) + " Kč";


        }

        private void Admin_Open(object sender, RoutedEventArgs e)
        {

            AdminWindow aw = new AdminWindow();
            aw.Show();
        }
        
    }
}
