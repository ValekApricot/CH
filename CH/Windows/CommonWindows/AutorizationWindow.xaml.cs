﻿using CoffeeHouse.ClassHelper;
using CoffeeHouse.Windows.Director;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static CoffeeHouse.ClassHelper.EFClass;

namespace CoffeeHouse.Windows.CommonWindows
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
        }

        private void BtnToReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow= new RegistrationWindow();
            registrationWindow.Show();
            Close();
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            var OneGuest = EFClass.Context.Guest.ToList().Where(i=>i.Login==TbLogin.Text && i.Password==TbPassword.Password).FirstOrDefault();
            if (OneGuest!=null)
            {
                DefaultDirectorWindow defaultDirectorWindow = new DefaultDirectorWindow();
                defaultDirectorWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void TbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text=="Login")
            {
                TbLogin.Text = "";
            }
        }

        private void TbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text == "")
            {
                TbLogin.Text = "Login";
            }
        }

        private void TbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbPassword.Password == "")
            {
                TbPassword.Password = "Password";
            }
        }

        private void TbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbPassword.Password == "Password")
            {
                TbPassword.Password = "";
            }
        }
    }
}
