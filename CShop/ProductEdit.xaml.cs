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
using System.Windows.Shapes;

namespace CShop
{
    /// <summary>
    /// Interakční logika pro ProductEdit.xaml
    /// </summary>
    public partial class ProductEdit : Window
    {
        internal Managers.Product manager;
        public ProductEdit()
        {
            InitializeComponent();
            Container container = Container.Instance;
            this.manager = container.Get<Managers.Product>("manager.product");
            categories.ItemsSource = container.Get<Managers.ProductCategory>("manager.productCategory").getAll();
        }

        private void ProductSaveClick(object sender, RoutedEventArgs e)
        {
            string name = this.name.Text;
            double price = Convert.ToDouble(this.price.Text);
            TextRange textRange = new TextRange(this.description.Document.ContentStart, this.description.Document.ContentEnd);
            string description = textRange.Text;
            Models.ProductCategory category = (Models.ProductCategory)(this.categories.SelectedItem);
            if (name.Length > 0)
            {
                Models.Product product = new Models.Product();
                product.Name = name;
                product.Category = category;
                product.Price = price;
                product.Description = description;

                manager.create(product);
                this.Close();
            }
        }
    }
}
