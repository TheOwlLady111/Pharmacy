using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;
using Pharmacy.Models;

namespace Pharmacy.ViewModels
{
    public class ExpiryMedItemsViewModel : BaseViewModel
    {
        public ObservableCollection<MedItemViewModel> ExpiryMedItems { get; set; }
        private bool _empty;
        private bool _notEmpty;
        
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
        public RelayCommand DeleteExpiries { get; set; }
        
        public ExpiryMedItemsViewModel()
        {

            ExpiryMedItems = new ObservableCollection<MedItemViewModel>(
             Service.GetExpiryMedItems.Select(item => new MedicamentViewModel(item)));
            if (ExpiryMedItems.Count!=0) { IsNotEmpty = true; IsEmpty = false; }
            else { IsEmpty = true; IsNotEmpty = false; }

            DeleteExpiries = new RelayCommand(o =>
            {
                Service.DeleteItems(Service.GetExpiryMedItems);
                IsEmpty = true; IsNotEmpty = false;
                ExpiryMedItems = null;
            });
        }
    }
}
