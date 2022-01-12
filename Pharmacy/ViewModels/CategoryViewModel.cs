using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pharmacy.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private Category _category;

        private MedItemViewModel _selectedMedItem;
        private MedItemViewModel _newItem;

        public bool AddAction;
        public bool EditAction;
        public bool isMedicament;
        public bool isInstrument;
        public ObservableCollection<MedItemViewModel> Items { get; set; }

        public RelayCommand DeleteMedItem { set; get; }
        public RelayCommand AddMedicament { get; set; }
        public RelayCommand AddInstrument { get; set; }
        public RelayCommand EditItem { get; set; }

        private object _currentViewModel;

        public Category GetCat
        {
            get
            {
                return _category;
            }
        }

        public MedItemViewModel NewItem
        {
            get
            {
                return _newItem;
            }

            set
            {
                _newItem = value;
                OnPropertyChanged("NewItem");
            }
        }

        public object CurrentVM
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public MedItemViewModel SelectedMedItem
        {
            get
            {
                return _selectedMedItem;
            }
            set
            {
                _selectedMedItem = value;
                CurrentVM = _selectedMedItem;
                OnPropertyChanged("SelectedMedItem");
            }
        }

        public string Name
        {
            get
            {
                return _category.Name;
            }
            set
            {
                _category.Name = value;
                OnPropertyChanged();
            }
        }

        public CategoryViewModel()
        {
            
            _category = new Category();
            Items = new ObservableCollection<MedItemViewModel>();
            DeleteMedItem = new RelayCommand(o =>
            {
                foreach (User user in Service.GetUsers)
                {
                   for (int i=0; i<user.PillsMeals.Count; i++)
                    {
                        if (user.PillsMeals[i].Pill.Name == SelectedMedItem.NameMedItem)
                        {
                            user.PillsMeals.Remove(user.PillsMeals[i]);
                            i--;
                        }
                    }
                }
                Service.RemoveProduct(SelectedMedItem.NameMedItem);
                Items.Remove(SelectedMedItem);
                SelectedMedItem = null;
            });

            AddMedicament = new RelayCommand(o =>
            {
                AddAction = true;
                EditAction = false;
                isMedicament = true;
                isInstrument = false;
                MedItemOptionsViewModel model = new MedItemOptionsViewModel(this);
                CurrentVM = model;

            }
              );
            AddInstrument = new RelayCommand(o =>
            {
                AddAction = true;
                EditAction = false;
                isInstrument = true;
                isMedicament = false;
                MedItemOptionsViewModel model = new MedItemOptionsViewModel(this);
                CurrentVM = model;

            }
              );
            EditItem = new RelayCommand(o =>
            {
                AddAction = false;
                EditAction = true;
                if (SelectedMedItem is InstrumentForMedicineViewModel)
                {
                  
                    isInstrument = true;
                    isMedicament = false;
                }
                else
                {
                   
                    isMedicament = true;
                    isInstrument = false;
                }
                MedItemOptionsViewModel model = new MedItemOptionsViewModel(this);
                model.NameMedItem = SelectedMedItem.NameMedItem;
                model.ReleaseDate = SelectedMedItem.ReleaseDate;
                model.ExpiryDate = SelectedMedItem.ExpiryDate;
                model.Manufacture = SelectedMedItem.Manufacture;
                model.WhatFor = SelectedMedItem.WhatFor;
                model.Ammount = SelectedMedItem.Ammount;
                model.Link = SelectedMedItem.Link;
            if (SelectedMedItem is InstrumentForMedicineViewModel)
            { model.Type = (SelectedMedItem as InstrumentForMedicineViewModel).Type;
                    model.isInstrument = true;
                }
               else
                {
                    model.Doze = (SelectedMedItem as MedicamentViewModel).Doze;
                    model.isMedicament = true;
                }
                CurrentVM = model;

            }
                );
        }
        public CategoryViewModel(Category cat) : this()
        {
            _category = cat;
        
            if (_category != null)
            {
                if (cat.Pharmacy != null && cat.Pharmacy.Count!=0)
                {
                   // Items = new ObservableCollection<MedItemViewModel>();
                    if ( cat.Pharmacy[0] is Medicament)
                    {

                        Items = new ObservableCollection<MedItemViewModel>(
                         Service.GetMedItemsInCategory(_category.Name).Select(item => new MedicamentViewModel(item)));
                    }
                    else
                    {
                        if (cat.Pharmacy[0] is InstumentForMedicine)
                        {

                            Items = new ObservableCollection<MedItemViewModel>(
                             Service.GetMedItemsInCategory(_category.Name).Select(item => new InstrumentForMedicineViewModel(item)));
                        }
                    }
                }
               }
            }
        }
    }
    

