﻿using CoffeeHouse.Windows.CommonWindows;
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
using static CoffeeHouse.ClassHelper.CartClass;

namespace CoffeeHouse.Windows.CommonWindows
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            GetProductList();
        }
        void GetProductList()
        {
            LvProductList.ItemsSource = stuffsCart.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
            Close();
        }

        private void btnRemoveOnCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            DataBase.Stuff selectedProduct = button.DataContext as DataBase.Stuff;

            if (selectedProduct != null)
            {
                if (selectedProduct.Quantity==1 || selectedProduct.Quantity == 0)
                {
                    stuffsCart.Remove(selectedProduct);
                }
                else
                {
                    selectedProduct.Quantity--;
                    int o = stuffsCart.IndexOf(selectedProduct);
                    stuffsCart.Remove(selectedProduct);
                    stuffsCart.Insert(o, selectedProduct);
                }
                
            }
            GetProductList();
        }

        private void btnAddOnCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            DataBase.Stuff selectedProduct = button.DataContext as DataBase.Stuff;
            if (selectedProduct != null)
            {
                selectedProduct.Quantity++;
                int o = stuffsCart.IndexOf(selectedProduct);
                stuffsCart.Remove(selectedProduct);
                stuffsCart.Insert(o, selectedProduct);
            }
            GetProductList();
        }
    }
}
