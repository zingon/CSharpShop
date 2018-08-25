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
    /// Interakční logika pro ProductCategoryEdit.xaml
    /// </summary>
    public partial class ProductCategoryEdit : Window
    {
        internal Managers.ProductCategory manager;
        public ProductCategoryEdit()
        {
            InitializeComponent();
            Container container = Container.Instance;
            this.manager = container.Get<Managers.ProductCategory>("manager.productCategory");
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            string name = this.name.Text;
            if(name.Length > 0 )
            {
                Models.ProductCategory category = new Models.ProductCategory();
                category.Name = name;

                manager.create(category);
                this.Close();
            }
        }
    }
}
