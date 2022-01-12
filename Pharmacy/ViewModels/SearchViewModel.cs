using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Pharmacy.Helpers;
using Pharmacy.Models;

namespace Pharmacy.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {

        public ObservableCollection<MedItemViewModel> Items { get; set; }
        public CollectionViewSource FilteredItems { get; set; }


        private MedItemViewModel _selectedItem;

        private string _searchFilter;
        public MedItemViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    if (_selectedItem.Med is Medicament)
                    {
                        Medicament med = new Medicament();
                        med = _selectedItem.Med as Medicament;
                        _selectedItem = new MedicamentViewModel(med);
                    }
                    else
                    {
                        InstumentForMedicine med = new InstumentForMedicine();
                        med = _selectedItem.Med as InstumentForMedicine;
                        _selectedItem = new InstrumentForMedicineViewModel(med);
                    }
                }

                OnPropertyChanged();
            }
        }
        public string SearchFilter
        {
            get => _searchFilter;
            set
            {
                _searchFilter = value;
                AddFilter();
                FilteredItems.View.Refresh();
            }
        }

        private void AddFilter()
        {
            FilteredItems.Filter += (object sender, FilterEventArgs e) =>
            {
                MedItemViewModel search = e.Item as MedItemViewModel;
                if (search != null)
                {
                    if (SearchFilter != null)
                    {
                        if (search.NameMedItem.ToLower().Contains(SearchFilter.ToLower())) e.Accepted = true;
                        else e.Accepted = false;
                    }
                }
            };
        }

        public SearchViewModel()
        {
            SelectedItem = null;
            Items = new ObservableCollection<MedItemViewModel>(Service.GetAllItems().Select(item =>new MedItemViewModel(item)));
            FilteredItems = new CollectionViewSource();
            FilteredItems.Source = Items; 
        }
    }
}
