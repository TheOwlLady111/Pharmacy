using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Helpers;

namespace Pharmacy.ViewModels
{
    public class InstrumentForMedicineViewModel : MedItemViewModel
    {
        public InstrumentForMedicineViewModel(MedItem item) : base(item)
        {
            
        }

        public string Type
        {
            get
            {
                return (_item as InstumentForMedicine).Type;
            }
            set
            {
                (_item as InstumentForMedicine).Type = value;
                OnPropertyChanged("Type");
            }
        }
    }
}
