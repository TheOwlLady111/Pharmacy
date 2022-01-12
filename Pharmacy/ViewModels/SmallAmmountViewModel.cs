using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;

namespace Pharmacy.ViewModels
{
    public class SmallAmmountViewModel : BaseViewModel
    {
        public ObservableCollection<MedItemViewModel> SmallAmmountItems { get; set; }
        private bool _empty;
        private bool _notEmpty;
        private MedicamentViewModel _selectedItem;
        private object _currentViewModel;

        public object CurrentVM
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public MedicamentViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }


        public bool IsEmpty
        {
            get
            {
                return _empty;
            }
            set
            {
                _empty = value;
                OnPropertyChanged("IsEmpty");
            }
        }

        public bool IsNotEmpty
        {
            get
            {
                return _notEmpty;
            }
            set
            {
                _notEmpty = value;
                OnPropertyChanged("IsNotEmpty");
            }
        }
        public RelayCommand ChangeAmmount1 { get; set; }

        public SmallAmmountViewModel()
        {

            SmallAmmountItems = new ObservableCollection<MedItemViewModel>(
             Service.SmallAmmountItems.Select(item => new MedicamentViewModel(item)));
            if (SmallAmmountItems.Count != 0) { IsNotEmpty = true; IsEmpty = false; }
            else { IsEmpty = true; IsNotEmpty = false; }

            ChangeAmmount1 = new RelayCommand(o =>
            {
                ChangeAmmountViewModel model = new ChangeAmmountViewModel(this);
                model.Ammount = SelectedItem.Ammount;

               

                CurrentVM = model;
               
            });
        }
    }
}
