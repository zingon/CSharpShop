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
    /// Interakční logika pro AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void CategoryEditClick(object sender, RoutedEventArgs e)
        {
            ProductCategoryEdit categoryEdit = new ProductCategoryEdit();
            categoryEdit.Show();
        }
        private void ProductEditClick(object sender, RoutedEventArgs e)
        {
            ProductEdit productEdit = new ProductEdit();
            productEdit.Show();
        }
    }
}
