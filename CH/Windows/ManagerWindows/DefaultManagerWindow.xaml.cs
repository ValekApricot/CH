﻿using CoffeeHouse.ClassHelper;
using CoffeeHouse.Windows.CommonWindows;
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
using static CoffeeHouse.ClassHelper.AuthUserClass;

namespace CoffeeHouse.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для DefaultManagerWindow.xaml
    /// </summary>
    public partial class DefaultManagerWindow : Window
    {
        public DefaultManagerWindow()
        {
            InitializeComponent();
            tbHeaderUser.Content = authEmploee.FullName + " | " + authEmploee.Post.Title;
        }

        private void btnProductList_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
            Close();
        }
    }
}
