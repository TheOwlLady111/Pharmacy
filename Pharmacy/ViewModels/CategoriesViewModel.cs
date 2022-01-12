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
    public class CategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryViewModel> categories { get; set; }
        private CategoryViewModel _selectedCategory;
        private CategoryViewModel _newCategory;
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

        public RelayCommand DeleteCategory { set; get; }
        public RelayCommand AddCategory { set; get; }

        public RelayCommand EditCategory { get; set; }

        public CategoryViewModel NewCategory
        {
            get
            {
                return _newCategory;
            }
            set
            {
                _newCategory = value;
                OnPropertyChanged("NewCategory");
            }
        }
        public CategoryViewModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
               
                _selectedCategory = value;
                CurrentVM = _selectedCategory;
                OnPropertyChanged("SelectedCategory");
            }
        }


        public CategoriesViewModel()
        {
            //SelectedCategory = new CategoryViewModel();
            //CurrentVM = SelectedCategory;
            if (_selectedCategory != null)
            { SelectedCategory = new CategoryViewModel(Service.GetCategory(_selectedCategory.Name));
                CurrentVM = SelectedCategory;
            }

            categories = new ObservableCollection<CategoryViewModel>();
            categories = new ObservableCollection<CategoryViewModel>(
                  Service.GetListOfCategories.Select(cat => new CategoryViewModel(cat)));

            DeleteCategory = new RelayCommand(o =>
            {
                
                Service.RemoveCategory(ToCategory());
                categories.Remove(SelectedCategory);
                SelectedCategory = null;
            });

            AddCategory = new RelayCommand(o =>
           {
               AddAction = true;
               EditAction = false;
               CategoryOptionsViewModel model = new CategoryOptionsViewModel(this);
               CurrentVM = model;

           }
                );
            EditCategory = new RelayCommand(o =>
            {
                AddAction = false;
                EditAction = true;
                CategoryOptionsViewModel model = new CategoryOptionsViewModel(this);
                model.Text = SelectedCategory.Name;
                CurrentVM = model;
                NewCategory = SelectedCategory;
            }
            );
        }

        private Category ToCategory()
        {
            return Service.GetCategory(SelectedCategory.Name);
        }
    }
}
