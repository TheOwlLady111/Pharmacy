using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Helpers;
using System.IO;

namespace Pharmacy.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private int _countExpiry;
        private int _countSmall;
        static private string _path = "Date.txt";
        private DateTime _dateFromFile;
        private int _attendences;

        public bool CanShow { get; set; }
        
        public RelayCommand CategoriesViewCommand { get; set; }

        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand ExpiryViewCommand { get; set; }
        public RelayCommand SmallAmmountViewCommand { get; set; }
        public RelayCommand UsersViewCommand { get; set; }
        private CategoriesViewModel VMofCategories { get; set; }
        private SearchViewModel VMofSearch { get; set; }
        private ExpiryMedItemsViewModel VMofExpiry { get; set; }
        private SmallAmmountViewModel VMofSmallAmmount { get; set; }
        private UsersViewModel VMofUsers { get; set; }

        public int CountExpiry
        {
            get
            {
                return _countExpiry;
            }
            set
            {
                _countExpiry = value;
                OnPropertyChanged("CountExpiry");
            }
        }

        public int CountSmall
        {
            get
            {
                return _countSmall;
            }
            set
            {
                _countSmall = value;
                OnPropertyChanged("CountSmall");
            }
        }


        private object _thisView;

        public object ThisView
        {
            get { return _thisView; }
            set
            {
                _thisView = value;
                OnPropertyChanged();
            }
        }

        private void DeletePills()
        {
            if (File.Exists(_path))
            {
                string _text = File.ReadAllText(_path);
                string[] words = _text.Split(' ');
                _dateFromFile = new DateTime(Convert.ToInt32(words[0]), Convert.ToInt32(words[1]), Convert.ToInt32(words[2]));
                _attendences = Convert.ToInt32(words[3]);

            }
            else
            {

                string text = $"{DateTime.Now.Year} {DateTime.Now.Month} {DateTime.Now.Day} {0}";
                File.Create(_path).Close();
                File.AppendAllText(_path, text);
                _dateFromFile = DateTime.Now;
                _attendences = 0;
            }

            if (DateTime.Now.Date == _dateFromFile && _attendences == 0)
            {
                foreach (User user in Service.GetUsers)
                {
                    foreach (MedicineMeal meal in user.PillsMeals)
                    {
                        if (Service.GetMedItem(meal.Pill.Name) != null)
                        {
                            int number = Service.GetMedItem(meal.Pill.Name).Ammount - meal.Ammount;
                            if (number >= 0)
                            {
                                Service.GetMedItem(meal.Pill.Name).Ammount = number;
                            }
                            else
                            {
                                Service.GetMedItem(meal.Pill.Name).Ammount = 0;
                            }
                        }
                    }
                }
                string text = $"{DateTime.Now.Year} {DateTime.Now.Month} {DateTime.Now.Day} {1}";
                File.Create(_path).Close();
                File.AppendAllText(_path, text);
            }
            else if (DateTime.Now > _dateFromFile)
            {
                int days = GetDays();
                foreach (User user in Service.GetUsers)
                {
                    foreach (MedicineMeal meal in user.PillsMeals)
                    {
                        if (Service.GetMedItem(meal.Pill.Name) != null)
                        {
                            int number = Service.GetMedItem(meal.Pill.Name).Ammount - meal.Ammount * days;
                            if (number >= 0)
                            {
                                Service.GetMedItem(meal.Pill.Name).Ammount = number;
                            }
                            else
                            {
                                Service.GetMedItem(meal.Pill.Name).Ammount = 0;
                            }
                        }
                    }
                }
                string text = $"{DateTime.Now.Year} {DateTime.Now.Month} {DateTime.Now.Day} {1}";
                File.Create(_path).Close();
                File.AppendAllText(_path, text);
            }


        }
        public MainWindowViewModel()
        {
            DeletePills();
    
            VMofCategories = new CategoriesViewModel();
            VMofSearch = new SearchViewModel();
            VMofExpiry = new ExpiryMedItemsViewModel();
            VMofSmallAmmount = new SmallAmmountViewModel();
            VMofUsers = new UsersViewModel();
            CountExpiry = Service.GetExpiryMedItems.Count;
            CountSmall = Service.SmallAmmountItems.Count;

            
            CategoriesViewCommand = new RelayCommand(o =>
            {
                CanShow = false;
                CountExpiry = Service.GetExpiryMedItems.Count;
                CountSmall = Service.SmallAmmountItems.Count;
                ThisView = VMofCategories;
               
            });

            SearchViewCommand = new RelayCommand(o =>
            {
                CanShow = true;
                CountExpiry = Service.GetExpiryMedItems.Count;
                CountSmall = Service.SmallAmmountItems.Count;
                ThisView = VMofSearch;
            });

            ExpiryViewCommand = new RelayCommand(o =>
            {
                CanShow = true;
                CountExpiry = Service.GetExpiryMedItems.Count;
                CountSmall = Service.SmallAmmountItems.Count;
                ThisView = VMofExpiry;
            });

            SmallAmmountViewCommand = new RelayCommand(o =>
            {
                CanShow = true;
                CountExpiry = Service.GetExpiryMedItems.Count;
                CountSmall = Service.SmallAmmountItems.Count;
                ThisView = VMofSmallAmmount;
            });
            UsersViewCommand = new RelayCommand(o =>
            {
                CanShow = true;
                CountExpiry = Service.GetExpiryMedItems.Count;
                CountSmall = Service.SmallAmmountItems.Count;
                ThisView = VMofUsers;
            });
        }

        private int GetDays()
        {
            int days = 0;
            if (DateTime.Now.Year == _dateFromFile.Year && DateTime.Now.Month == _dateFromFile.Month && DateTime.Now.Day != _dateFromFile.Day)
            {
                days = DateTime.Now.Day - _dateFromFile.Day;
            }
            else if (DateTime.Now.Year == _dateFromFile.Year && DateTime.Now.Month != _dateFromFile.Month)
            {
                days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day + DateTime.DaysInMonth(_dateFromFile.Year, _dateFromFile.Month) - _dateFromFile.Day;
            }
            return days;
        }
    }
}
