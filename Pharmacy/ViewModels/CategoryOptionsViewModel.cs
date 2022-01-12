using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Helpers;
using System.ComponentModel;

namespace Pharmacy.ViewModels
{
    public class CategoryOptionsViewModel:BaseViewModel, IDataErrorInfo
    {
        private string _text;

        private Category _cat;
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

                _cat.Name = _text;
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            { return _cat.IsValid; }
        }
        public CategoryOptionsViewModel() { }
        public CategoryOptionsViewModel(CategoriesViewModel model)

        {
            isAdd = model.AddAction;
            isEdit = model.EditAction;
            _cat = new Category();
            _cat.Name = string.Empty;
            _cat.Pharmacy = new List<MedItem>();
            
            Add = new RelayCommand(o =>
            {
                
                CategoryViewModel item = new CategoryViewModel(_cat);
                model.categories.Add(item);
                Service.AddCategory(_cat);
                model.SelectedCategory = item;
               // model.CurrentVM = null;


            }
                );

            Edit = new RelayCommand(o =>
            {
                foreach(CategoryViewModel mod in model.categories)
                {
                    if (model.NewCategory.Name == mod.Name)
                    {
                        Service.GetCategory(model.NewCategory.Name).Name = Text;
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
                        _cat.Validate("NameNull", ref error);
                        _cat.Validate("NameLength", ref error);
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
