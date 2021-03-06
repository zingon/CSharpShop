﻿using System;
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
        private Managers.Product productManager;
        private Managers.Order orderManager;
        public AdminWindow()
        {
            InitializeComponent();
            Container container = Container.Instance;
            this.productManager = container.Get<Managers.Product>("manager.product");
            this.orderManager = container.Get<Managers.Order>("manager.order");
            

            productsDataGrid.ItemsSource = productManager.getAll();
            orderDataGrid.ItemsSource = orderManager.getAll();

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

        private void ChangeState(object sender, RoutedEventArgs e)
        {
            Models.Order model = (sender as Button).DataContext as Models.Order;
            model.State = (sender as Button).Tag as string;

            this.orderManager.update(model);
            orderDataGrid.ItemsSource = orderManager.getAll();
        }




        private void EditProduct(object sender, RoutedEventArgs e)
        {
            Models.Product model = (sender as Button).DataContext as Models.Product;

            ProductEdit productEdit = new ProductEdit();
            productEdit.SetData(model);
            productEdit.Show();
            
        }

        private void RemoveProduct(object sender, RoutedEventArgs e)
        {
            Models.Product model = (sender as Button).DataContext as Models.Product;
            this.productManager.remove(model);
            productsDataGrid.ItemsSource = productManager.getAll();
        }


        private void ReloadDatagrid(object sender, RoutedEventArgs e)
        {
            productsDataGrid.ItemsSource = productManager.getAll(); 
        }
        
    }
}
