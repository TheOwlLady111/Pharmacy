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
    public class UsersViewModel : BaseViewModel
    {
        public ObservableCollection<UserViewModel> Users { get; set; }
        private UserViewModel _selectedUser;
        private UserViewModel _newUser;
        private object _currentVM;
        public bool AddAction;
        public bool EditAction;
        public object CurrentVM
        {
            get => _currentVM;
            set
            {
                _currentVM = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DeleteUser { set; get; }
        public RelayCommand AddUser { set; get; }

        public RelayCommand EditUser { get; set; }

        public UserViewModel NewUser
        {
            get
            {
                return _newUser;
            }
            set
            {
                _newUser = value;
                OnPropertyChanged("NewUser");
            }
        }
        public UserViewModel SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {

                _selectedUser = value;
                CurrentVM = _selectedUser;
                OnPropertyChanged("SelectedUser");
            }
        }


        public UsersViewModel()
        {
            if (_selectedUser != null)
            {
                SelectedUser = new UserViewModel(Service.GetUser(_selectedUser.Name));
            }

            Users = new ObservableCollection<UserViewModel>(
                  Service.GetUsers.Select(user => new UserViewModel(user)));

            DeleteUser = new RelayCommand(o =>
            {

                Service.RemoveUser(SelectedUser.Name);
                Users.Remove(SelectedUser);
                SelectedUser = null;
            });

            AddUser = new RelayCommand(o =>
            {
                AddAction = true;
                EditAction = false;
                UserOptionsViewModel model = new UserOptionsViewModel(this);
                CurrentVM = model;

            }
                );
            EditUser = new RelayCommand(o =>
            {
                AddAction = false;
                EditAction = true;
                UserOptionsViewModel model = new UserOptionsViewModel(this);
                model.Text = SelectedUser.Name;
                CurrentVM = model;
                NewUser = SelectedUser;
            }
            );
        }

        private User ToUser()
        {
            return Service.GetUser(SelectedUser.Name);
        }
    }
}

