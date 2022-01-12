using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;
using Pharmacy.Models;

namespace Pharmacy.ViewModels
{
    public class UserOptionsViewModel : BaseViewModel, IDataErrorInfo
    {
        private string _text;

        private User _user;

        public bool isAdd { get; set; }
        public bool isEdit { get; set; }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;

                _user.Name = _text;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            { return _user.IsValid; }
        }
        public UserOptionsViewModel() { }
        public UserOptionsViewModel(UsersViewModel model)
        {
            isAdd = model.AddAction;
            isEdit = model.EditAction;
            _user = new User();
            _user.Name = string.Empty;

            Add = new RelayCommand(o =>
            {

                UserViewModel item = new UserViewModel(_user);
                model.Users.Add(item);
                Service.AddUser(_user);
            }
                );

            Edit = new RelayCommand(o =>
            {
                foreach (UserViewModel mod in model.Users)
                {
                    if (model.NewUser.Name == mod.Name)
                    {
                        Service.GetUser(model.NewUser.Name).Name = Text;
                        mod.Name = Text;
                        model.CurrentVM = mod;
                        break;
                    }
                }
                

            }
                );
        }

        public RelayCommand Add { get; set; }

        public RelayCommand Edit { get; set; }

        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case "Text":
                        _user.Validate("NameUserNull", ref error);
                        _user.Validate("NameUserLength", ref error);
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

