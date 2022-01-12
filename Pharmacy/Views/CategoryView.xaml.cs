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
    /// Логика взаимодействия для CategoryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        private void items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (items.SelectedItem != null)
            {
                Delete1.IsEnabled = true;
                Edit1.IsEnabled = true;
            }
            else
            {
                Delete1.IsEnabled = false;
                Edit1.IsEnabled = false;
            }
        }

        private void Add1_Click(object sender, RoutedEventArgs e)
        {
            addIns.Visibility = Visibility.Visible;
            addMed.Visibility = Visibility.Visible;
        }
    }
}
