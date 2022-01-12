using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;
using Pharmacy.Models;

namespace Pharmacy.ViewModels
{
        public class MedicamentViewModel : MedItemViewModel
       {
            public MedicamentViewModel(MedItem item) : base(item)
            {
           
            }

            public double Doze
            {
                get
                {
                    return (_item as Medicament).Doze;
                }
                set
                {
                    (_item as Medicament).Doze = value;
                    OnPropertyChanged("Doze");
                }
            }
        }
    
}
