using MaterialDesignThemes.Wpf;
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
using Pharmacy.Helpers;

namespace Pharmacy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        public MainWindow()
        {
            InitializeComponent();
        }

   

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Service.SavePharmacy();
            Service.SaveUsers();
            Application.Current.Shutdown();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            brd.Visibility = Visibility.Hidden;
            brd1.Visibility = Visibility.Hidden;
            brd2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            brd.Visibility = Visibility.Hidden;
            brd1.Visibility = Visibility.Hidden;
            brd2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            brd.Visibility = Visibility.Hidden;
            brd1.Visibility = Visibility.Hidden;
            brd2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            brd.Visibility = Visibility.Hidden;
            brd1.Visibility = Visibility.Hidden;
            brd2.Visibility = Visibility.Hidden;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            brd.Visibility = Visibility.Hidden;
            brd1.Visibility = Visibility.Hidden;
            brd2.Visibility = Visibility.Hidden;
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}
