using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;
using Pharmacy.Models;
using System.ComponentModel;

namespace Pharmacy.ViewModels
{
   public class MedItemOptionsViewModel : BaseViewModel, IDataErrorInfo
    {
        public bool isMedicament { get; set; }
        public bool isInstrument { get; set; }

        public bool isAdd { get; set; }
        public bool isEdit { get; set; }

        private MedItemViewModel _model;
        private MedItem _medItem;
        private string _name=string.Empty;
        private DateTime _releaseDate = DateTime.Now;
        private DateTime _expiryDate=DateTime.Now;
        private string _manufacture = string.Empty;
        private string _whatFor = string.Empty;
        private int _ammount = 0;
        private string _link = string.Empty;
        private double _doze = 0;
        private string _type = string.Empty;

        public RelayCommand AddItem { get; set; }
        public RelayCommand EditItem { get; set; }

        public bool IsValid
        {
            get
            { return _medItem.IsValid; }
        }

        public MedItemViewModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }

        public string NameMedItem
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                _medItem.Name = _name;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _releaseDate;
            }
            set
            {
                _releaseDate = value;
                _medItem.ReleaseDate = _releaseDate;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public DateTime ExpiryDate
        {
            get
            {
                return _expiryDate;
            }
            set
            {
                _expiryDate = value;
                _medItem.ExpiryDate = _expiryDate;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public string Manufacture
        {
            get
            {
                return _manufacture;
            }
            set
            {
                _manufacture = value;
                _medItem.Manufacture = _manufacture;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public string WhatFor
        {
            get
            {
                return _whatFor;
            }
            set
            {
                _whatFor = value;
                _medItem.WhatFor = _whatFor; 
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public int Ammount
        {
            get
            {
                return _ammount;
            }
            set
            {
                _ammount = value;
                _medItem.Ammount = _ammount;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                _medItem.Link = _link;
               
                OnPropertyChanged("Link");
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
               ( _medItem as InstumentForMedicine).Type = _type;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public double Doze
        {
            get
            {
                return _doze;
            }
            set
            {
                _doze = value;
                (_medItem as Medicament).Doze = _doze;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public MedItemOptionsViewModel (CategoryViewModel viewModel)
        {
            isAdd = viewModel.AddAction;
            isEdit = viewModel.EditAction;

            
            
            if (viewModel.isMedicament)
            {
                isMedicament = true;
                _medItem = new Medicament(_name, _releaseDate, _expiryDate, _manufacture, _whatFor, _ammount, _link, _doze);
                
            }
            else if (viewModel.isInstrument)
            {
                isInstrument = true;
                _medItem = new InstumentForMedicine(_name, _releaseDate, _expiryDate, _manufacture, _whatFor, _ammount, _link, _type);
            }

           

            AddItem = new RelayCommand(o =>
               {
                   if (isMedicament)
                   { Model = new MedicamentViewModel(_medItem); }
                   else
                   {
                       Model = new InstrumentForMedicineViewModel(_medItem);
                   }
                   viewModel.Items.Add(Model);
                   Service.AddProduct(viewModel.GetCat.Name, _medItem);
               });

            EditItem = new RelayCommand(o=>
            { 
            foreach(MedItemViewModel item in viewModel.Items)
                {
                    if (item.NameMedItem == viewModel.SelectedMedItem.NameMedItem)
                    {
                        item.NameMedItem = NameMedItem;
                        item.ReleaseDate = ReleaseDate;
                        item.ExpiryDate = ExpiryDate;
                        item.Manufacture = Manufacture;
                        item.WhatFor = WhatFor;
                        item.Ammount = Ammount;
                        item.Link = Link;
                        Service.GetMedItem(item.NameMedItem).Name = NameMedItem;
                        Service.GetMedItem(item.NameMedItem).ReleaseDate = ReleaseDate;
                        Service.GetMedItem(item.NameMedItem).ExpiryDate = ExpiryDate;
                        Service.GetMedItem(item.NameMedItem).Manufacture = Manufacture;
                        Service.GetMedItem(item.NameMedItem).WhatFor = WhatFor;
                        Service.GetMedItem(item.NameMedItem).Ammount = Ammount;
                        Service.GetMedItem(item.NameMedItem).Link = Link;
                        if (isInstrument)
                        {
                            (item as InstrumentForMedicineViewModel).Type = Type;
                            (Service.GetMedItem(item.NameMedItem) as InstumentForMedicine).Type = Type;
                        }
                        if (isMedicament)
                        {
                            (item as MedicamentViewModel).Doze = Doze;
                            (Service.GetMedItem(item.NameMedItem) as Medicament).Doze = Doze;
                        }

                        viewModel.CurrentVM = item;
                        break;
                    }
                }
            });


        }
        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                if (isInstrument)
                { switch (propertyName)
                    {
                        case "NameMedItem":
                            _medItem.Validate("NameItemNull", ref error);
                            _medItem.Validate("NameItemLength", ref error);
                            break;
                        case "ReleaseDate":
                            _medItem.Validate("ReleaseDate", ref error);
                            _medItem.Validate("CompareDates", ref error);
                            break;
                        case "ExpiryDate":
                            _medItem.Validate("ExpiryDate", ref error);
                            _medItem.Validate("CompareDates", ref error);
                            break;
                        case "Manufacture":
                            _medItem.Validate("ManufactureItemNull", ref error);
                            _medItem.Validate("ManufactureItemLength", ref error);
                            break;
                        case "WhatFor":
                            _medItem.Validate("WhatForItemNull", ref error);
                            _medItem.Validate("WhatForItemLength", ref error);
                            break;
                        case "Ammount":
                            _medItem.Validate("AmmountRule", ref error);
                            break;

                        case "Type":
                            _medItem.Validate("TypeNull", ref error);
                            _medItem.Validate("TypeLength", ref error);
                            break;
                        
                    } }
                else
                {
                    switch (propertyName)
                    {
                        case "NameMedItem":
                            _medItem.Validate("NameItemNull", ref error);
                            _medItem.Validate("NameItemLength", ref error);
                            break;
                        case "ReleaseDate":
                            _medItem.Validate("ReleaseDate", ref error);
                            _medItem.Validate("CompareDates", ref error);
                            break;
                        case "ExpiryDate":
                            _medItem.Validate("ExpiryDate", ref error);
                            _medItem.Validate("CompareDates", ref error);
                            break;
                        case "Manufacture":
                            _medItem.Validate("ManufactureItemNull", ref error);
                            _medItem.Validate("ManufactureItemLength", ref error);
                            break;
                        case "WhatFor":
                            _medItem.Validate("WhatForItemNull", ref error);
                            _medItem.Validate("WhatForItemLength", ref error);
                            break;
                        case "Ammount":
                            _medItem.Validate("AmmountRule", ref error);
                            break;
                        case "Doze":
                            _medItem.Validate("DozeRule", ref error);
                            break;

                    }
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

    }
}
