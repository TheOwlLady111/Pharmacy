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

namespace Pharmacy.Views
{
    /// <summary>
    /// Логика взаимодействия для UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cats.SelectedItem != null)
            {
                Delete.IsEnabled = true;
                Edit.IsEnabled = true;
            }
            else
            {
                Delete.IsEnabled = false;
                Edit.IsEnabled = true;
            }
        }
    }
}
