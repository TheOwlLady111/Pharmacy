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
   public class UserViewModel : BaseViewModel
    {
        private User _user;
        private MedicineMeal _selectedMedItem;
      

        public bool AddAction;
        public bool EditAction;
      
        public ObservableCollection<MedicineMeal> Items { get; set; }

        public RelayCommand DeletePill { set; get; }
        public RelayCommand AddPill { get; set; }
        public RelayCommand EditPill { get; set; }

        private object _currentViewModel;

        public User GetUser
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged("GetUser");
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

        public MedicineMeal SelectedMedItem
        {
            get
            {
                return _selectedMedItem;
            }
            set
            {
                _selectedMedItem = value;
                OnPropertyChanged("SelectedMedItem");
            }
        }

        public string Name
        {
            get
            {
                return _user.Name;
            }
            set
            {
                _user.Name = value;
                OnPropertyChanged();
            }
        }

        public UserViewModel()
        {

            _user = new User();
            Items = new ObservableCollection<MedicineMeal>();
            DeletePill = new RelayCommand(o =>
            {
                Service.GetUser(_user.Name).PillsMeals.Remove(Service.GetMedFromUser(_user.Name, SelectedMedItem.Pill.Name));
                Items.Remove(SelectedMedItem);
                SelectedMedItem = null;
                
            });

            AddPill = new RelayCommand(o =>
            {
                AddAction = true;
                EditAction = false;

                UserPillsOptionsViewModel model = new UserPillsOptionsViewModel(this);
                CurrentVM = model;
                

            }
              );
           
            EditPill = new RelayCommand(o =>
            {
                AddAction = false;
                EditAction = true;
               
                UserPillsOptionsViewModel model = new UserPillsOptionsViewModel(this);
                model.NameMedItem = SelectedMedItem.Pill.Name;
              
                model.Ammount = SelectedMedItem.Ammount;
            
                CurrentVM = model;

                Items = new ObservableCollection<MedicineMeal>(GetUser.PillsMeals);


            }
                );
        }
        public UserViewModel(User user) : this()
        {
            _user = user;

            if (_user != null)
            {
                if (_user.PillsMeals != null)
                {
                        Items = new ObservableCollection<MedicineMeal>(
                         _user.PillsMeals);
                }
            }
        }
    }
}
