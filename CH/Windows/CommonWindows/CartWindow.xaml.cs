﻿using CoffeeHouse.Windows.CommonWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static CoffeeHouse.ClassHelper.EFClass;

namespace CoffeeHouse.Windows.CommonWindows
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        bool PriseOff = false;
        public CartWindow()
        {
            InitializeComponent();
            GetProductList();
            DiscountTHU(DateTime.Now, Convert.ToDouble(tbAllCost.Text));
            //stuffsCart = DiscountTHU(DateTime.Now, tbAllCost.Text,stuffsCart);
        }
        void GetProductList()
        {
            LvProductList.ItemsSource = stuffsCart.ToList();
            tbAllCost.Text = "0";
            foreach (var item in stuffsCart)
            {
                tbAllCost.Text = Convert.ToString(Convert.ToDouble(tbAllCost.Text) + Convert.ToDouble(item.Price) * item.Quantity);
            }
        }

        void DiscountTHU(DateTime dateTime, double Cst)
        {
            double day = dateTime.Day;
            string dayOfWeek = dateTime.DayOfWeek.ToString();


            if ((day / 7) > 0 && dayOfWeek == "Thursday")
            {
                tbCostText.Text = "Цена с учётом скидки";
                tbCostText.Width = 400;
                for (int i = 0; i < stuffsCart.Count; i++)
                {
                    stuffsCart[i].Price -= Convert.ToDecimal(Convert.ToDouble((stuffsCart[i].Price)) * 0.04);
                }
                GetProductList();
                PriseOff = true;
            }
        }



        //----------------StupidUnlessMethod

        //public static ObservableCollection<DataBase.Stuff> DiscountTHU(DateTime dateTime, string Cst, ObservableCollection<DataBase.Stuff> startUnlessCollection)
        //{
        //    Double.TryParse(Cst, out double DCst) ;
        //    double day = dateTime.Day;
        //    string dayOfWeek = dateTime.DayOfWeek.ToString();
        //    ObservableCollection<DataBase.Stuff> UslessCollection = startUnlessCollection;

        //    if ((day / 7) > 0 && dayOfWeek == "Thursday")
        //    {
        //        for (int i = 0; i < startUnlessCollection.Count; i++)
        //        {
        //            UslessCollection[i].Price -= Convert.ToDecimal(Convert.ToDouble((UslessCollection[i].Price)) * 0.04);
        //        }
        //    }
        //    return UslessCollection;
        //}


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
                if (selectedProduct.Quantity == 1 || selectedProduct.Quantity == 0)
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

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataBase.Check check = new DataBase.Check();
                check.IDEmployee = ClassHelper.AuthUserClass.authEmploee.IDEmploee;
                check.IDGuest = 1;
                check.Date = DateTime.Now;
                if (check != null)
                {
                    Context.Check.Add(check);
                    Context.SaveChanges();
                }

                if (stuffsCart.Count > 0)
                {


                    foreach (var item in stuffsCart)
                    {
                        DataBase.StuffList stuffList = new DataBase.StuffList();
                        stuffList.IDStuff = item.IDStuff;
                        stuffList.Quantity = item.Quantity;
                        stuffList.IDCheck = Context.Check.ToList().LastOrDefault().IDCheck;
                        Context.StuffList.Add(stuffList);
                        Context.SaveChanges();
                    }
                    MessageBox.Show("Продукты успешно добавлены");


                    if (PriseOff)
                    {
                        for (int i = 0; i < stuffsCart.Count; i++)
                        {
                            stuffsCart[i].Price += Convert.ToDecimal(Convert.ToDouble(((stuffsCart[i].Price)) / 96) * 4);
                        }
                        Context.SaveChanges();
                    }

                    ProductListWindow productListWindow = new ProductListWindow();
                    productListWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("В корзине нет продукции");
                    ProductListWindow productListWindow = new ProductListWindow();
                    productListWindow.Show();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Возникла неизвесная ошибка");
            }

        }
    }
}
