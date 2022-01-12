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
    /// Логика взаимодействия для SmallAmmountView.xaml
    /// </summary>
    public partial class SmallAmmountView : UserControl
    {
        public SmallAmmountView()
        {
            InitializeComponent();
        }

        private void name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (name.SelectedItem != null)
            {
                but.IsEnabled = true;
            }
            else
            {
                but.IsEnabled = false;
            }
        }
    }
}
