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
using Pharmacy.ViewModels;

namespace Pharmacy.Views
{
    /// <summary>
    /// Логика взаимодействия для MedItemAddView.xaml
    /// </summary>
    public partial class MedItemAddView : UserControl
    {
        public MedItemAddView()
        {

            InitializeComponent();
            //if ((DataContext as MedItemOptionsViewModel).isEdit)
            //{ Medicament.Visibility = Visibility.Collapsed;
            //    Instrument.Visibility = Visibility.Collapsed;
            //        }
        }

       

        private void Medicament_Click(object sender, RoutedEventArgs e)
        {
            
            (DataContext as MedItemOptionsViewModel).isMedicament = true;
             doze.Visibility = Visibility.Visible;
            type.Visibility = Visibility.Hidden;

            
        }

        private void Instrument_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MedItemOptionsViewModel).isInstrument = true;
            type.Visibility = Visibility.Visible;
            doze.Visibility = Visibility.Hidden;
        }

       

        //private void panel_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if ((DataContext as MedItemOptionsViewModel).isEdit)
        //    {
        //        panel.Visibility = Visibility.Hidden;
        //        if ((DataContext as MedItemOptionsViewModel).isMedicament)
        //        {
        //            doze.Visibility = Visibility.Visible;
        //            type.Visibility = Visibility.Hidden;

        //        }
        //        if ((DataContext as MedItemOptionsViewModel).isInstrument)
        //        {
        //            doze.Visibility = Visibility.Hidden;
        //            type.Visibility = Visibility.Visible;


        //        }

        //    }

        //    else if ((DataContext as MedItemOptionsViewModel).isAdd)
        //    {
        //        panel.Visibility = Visibility.Visible;


        //    }

        //}

    }
}
