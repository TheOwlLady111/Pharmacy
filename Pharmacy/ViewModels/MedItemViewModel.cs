using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;
using Pharmacy.Models;

namespace Pharmacy.ViewModels
{
    public class MedItemViewModel : BaseViewModel
    {
        protected MedItem _item;

        public MedItem Med
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }
        public string NameMedItem
        {
            get
            {
                return _item.Name;
            }
            set
            {
                _item.Name = value;
                OnPropertyChanged("NameMedItem");
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _item.ReleaseDate.Date;
            }
            set
            {
                _item.ReleaseDate = value.Date;
                OnPropertyChanged("ReleaseDate");
            }
        }

        public DateTime ExpiryDate
        {
            get
            {
                return _item.ExpiryDate.Date;
            }
            set
            {
                _item.ExpiryDate = value;
                OnPropertyChanged("ExpiryDate");
            }
        }

        public string Manufacture
        {
            get
            {
                return _item.Manufacture;
            }
            set
            {
                _item.Manufacture = value;
                OnPropertyChanged("Manufacture");
            }
        }

        public string WhatFor
        {
            get
            {
                return _item.WhatFor;
            }
            set
            {
                _item.WhatFor = value;
                OnPropertyChanged("WhatFor");
            }
        }

        public int Ammount
        {
            get
            {
                return _item.Ammount;
            }
            set
            {
                _item.Ammount = value;
                OnPropertyChanged("Ammount");
            }
        }

        public string Link
        {
            get
            {
                return _item.Link;
            }
            set
            {
                _item.Link = value;
                OnPropertyChanged("Link");
            }
        }

      
        public MedItemViewModel(MedItem item) : this()
        {
            if (item != null)
            {
                if (item is Medicament)
                {
                    _item = new Medicament();

                }
                if (item is InstumentForMedicine)
                {
                    _item = new InstumentForMedicine();
                }
                _item = item;
            }
        }

        public MedItemViewModel()
        {
           
        }
    }
}
