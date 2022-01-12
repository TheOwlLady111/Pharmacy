using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;
using Pharmacy.Models;

namespace Pharmacy.ViewModels
{
    class ChangeAmmountViewModel:BaseViewModel, IDataErrorInfo
    {
        private int _ammount;

        private Medicament _item;

        public int Ammount
        {
            get
            {
                return _ammount;
            }
            set
            {
                _ammount = value;
                _item.Ammount = _ammount;
                
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            { return _item.IsValid; }
        }

        public ChangeAmmountViewModel() { }
        public ChangeAmmountViewModel(SmallAmmountViewModel model)
        {
            _item = new Medicament();
            _item.Name = model.SelectedItem.NameMedItem;
            _item.ExpiryDate = model.SelectedItem.ExpiryDate;
            _item.ReleaseDate = model.SelectedItem.ReleaseDate;
            _item.Ammount = model.SelectedItem.Ammount;
            _item.Manufacture = model.SelectedItem.Manufacture;
            _item.Doze = model.SelectedItem.Doze;
            _item.WhatFor = model.SelectedItem.WhatFor;

            SaveAmmount = new RelayCommand(o =>
            {

                foreach (MedItem mod in Service.GetAllItems())
                {
                    if (_item.Name == mod.Name)
                    {
                        Service.GetMedItem(_item.Name).Ammount = Ammount;
                        break;
                    }
                    if (_item.Ammount>4)
                    {
                        model.SmallAmmountItems.Remove(model.SelectedItem);
                    }
                    if (model.SmallAmmountItems.Count != 0) { model.IsNotEmpty = true; model.IsEmpty = false; }
                    else { model.IsEmpty = true; model.IsNotEmpty = false; }
                }
               

               
                model.CurrentVM = null;

            }
                );

            
        }

        public RelayCommand SaveAmmount { get; set; }

        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case "Ammount":
                        _item.Validate("AmmountRule", ref error);
                       
                        break;

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

