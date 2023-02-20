using CoffeeHouse.DataBase;
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


using CoffeeHouse.ClassHelper;
using CoffeeHouse.DataBase;
using static CoffeeHouse.ClassHelper.EFClass;


namespace CoffeeHouse.Windows.CommonWindows
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public ProductListWindow()
        {
            InitializeComponent();

            GetProducr();
        }
        private void GetProducr()
        {
            List<Stuff> stuffList = new List<Stuff>();

            stuffList = Context.Stuff.ToList();

            LvProductList.ItemsSource = stuffList;
        }
    }
}
