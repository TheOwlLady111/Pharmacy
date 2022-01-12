using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Helpers;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Pharmacy.ViewModels
{
    public class UserPillsOptionsViewModel : BaseViewModel, IDataErrorInfo
    {
       

        public bool isAdd { get; set; }
        public bool isEdit { get; set; }

        private MedicineMeal _model;
        private Medicament _medicament;
        private string _name = string.Empty;
        private int _ammount = 0;
        private string _str = string.Empty;

        public RelayCommand AddItem { get; set; }
        public RelayCommand EditItem { get; set; }

        public bool IsValid
        {
            get
            { return _model.IsValid; }
        }

        public MedicineMeal Model
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
                _medicament = Service.GetMedItem(_name) as Medicament;
                _model.Pill = _medicament;
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
                _model.Ammount = _ammount;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public string NameAndAmmountStr1
        {
            get
            {
                _str = $"{NameMedItem} : {Ammount} в день";
                return _str;
            }
            set
            {
                _str = value;
                OnPropertyChanged("NameAndAmmount");
            }
        }


        public UserPillsOptionsViewModel(UserViewModel viewModel)
        {
           

            isAdd = viewModel.AddAction;
            isEdit = viewModel.EditAction;



            _model = new MedicineMeal();


            AddItem = new RelayCommand(o =>
            {
                Model = new MedicineMeal(Ammount, _medicament);
                viewModel.Items.Add(Model);
                Service.AddMedMeal(viewModel.GetUser.Name, Model);
            });

            EditItem = new RelayCommand(o =>
            {
                foreach (MedicineMeal item in viewModel.Items)
                {
                    if (item.Pill.Name == viewModel.SelectedMedItem.Pill.Name)
                    {
                        item.Pill.Name = NameMedItem;
                        item.Ammount = Ammount;



                        Service.ChangeAmmount(viewModel.GetUser.Name, item.Pill.Name, Ammount);
                       
                        break;
                    }
                }  
               
                viewModel.CurrentVM = null;
            
                
            });


        }
        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                
                    switch (propertyName)
                    {
                        case "NameMedItem":
                            _model.Validate("Pill", ref error);
                            
                            break;
                        case "Ammount":
                            _model.Validate("Ammount", ref error);
                           
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
