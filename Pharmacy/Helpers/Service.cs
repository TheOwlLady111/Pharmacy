using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Data;

namespace Pharmacy.Helpers
{
    public class Service
    {
        static private MedItemDAO item = new MedItemDAO();
        static private CategoryDAO category = new CategoryDAO();
        // static private List<Category> listCat = new List<Category>();
        static private UserDAO user = new UserDAO();
        //static private List<User> _users = new List<User>();

        static public List<Category> GetListOfCategories
        {
            get
            {
                return category.GetCategories;
            }
        }

        static public Category GetCategory(string name)
        {
            return category.GetCategory(name);
        }

        static public Category GetCategory(MedItem med)
        {
            foreach (Category cat in category.GetCategories)
            {
                foreach (MedItem item in cat.Pharmacy)
                {
                    if (item == med) return cat;
                }
            }

            return null;
        }

        static public List<MedItem> GetMedItemsInCategory(string categoryName)
        {
            foreach (Category cat in category.GetCategories)
            {
                if (cat.Name == categoryName)
                {
                    return cat.Pharmacy;
                }
            }
            return null;
        }

        static public MedItem GetMedItem(string name)
        {
            return item.GetMedItem(name);
        }

        static public MedItem GetMedItem(string nameCat, string nameProd)
        {
            return item.GetMedItem(nameCat, nameProd);
        }

        static public void AddProduct(List<MedItem> list, MedItem med)
        {
            list.Add(med);
        }

        static public void AddProduct(string name, MedItem med)
        {
            Service.GetCategory(name).Add(med);
        }

        static public void RemoveProduct(List<MedItem> list, MedItem med)
        {
            list.Remove(med);
        }

        static public void RemoveProduct(MedItem med)
        {
            bool flag = true;
            foreach (Category cat in category.GetCategories)
            {
                for (int i = 0; i < cat.Pharmacy.Count; i++)
                {
                    if (med == cat.Pharmacy[i])
                    {
                        cat.Remove(cat.Pharmacy[i]);
                        flag = false;
                        break;
                        //int t = cat.Pharmacy.Count;
                        //i--;
                    }
                }
                if (!flag) break;
            }
        }

        static public void AddCategory(List<Category> list, Category cat)
        {
            list.Add(cat);
        }

        static public void AddCategory(Category cat)
        {
            category.Add(cat);
        }


        static public void RemoveCategory(Category cat)
        {
            foreach (Category categ in category.GetCategories)
            {
                if (categ == cat) category.GetCategories.Remove(categ);
                break;
            }
        }

        static public bool IsExpary(MedItem med)
        {
            if (med.CheckExpiryDate()) return true;
            else return false;
        }

        static public void ChangeAmmont(int dig, string name)
        {
            item.GetMedItem(name).ChangeAmmount(dig);
        }

        static public void RemoveProduct(string name)
        {
            item.RemoveItem(name);
        }

        static public List<MedItem> ListOfMedByName(string name)
        {
            List<MedItem> items = new List<MedItem>();
            foreach (Category cat in category.GetCategories)
            {
                foreach (MedItem item in cat.Pharmacy)
                {
                    if (item.Name == name) items.Add(item);
                }
            }
            return items;
        }

        static public List<MedItem> ListOfMedByManufacture(string manuf)
        {
            List<MedItem> items = new List<MedItem>();
            foreach (Category cat in category.GetCategories)
            {
                foreach (MedItem item in cat.Pharmacy)
                {
                    if (item.Manufacture == manuf) items.Add(item);
                }
            }
            return items;
        }

        static public List<MedItem> ListOfMedByWhatFor(string what)
        {
            List<MedItem> items = new List<MedItem>();
            foreach (Category cat in category.GetCategories)
            {
                foreach (MedItem item in cat.Pharmacy)
                {
                    if (item.WhatFor == what) items.Add(item);
                }
            }
            return items;
        }

        static public List<MedItem> GetAllItems()
        {
            List<MedItem> list = new List<MedItem>();
            foreach (Category cat in category.GetCategories)
            {
                if (cat.Pharmacy!=null)
                {
                    foreach (MedItem med in cat.Pharmacy)
                    {
                        list.Add(med);
                    }
                }
            }
            return list;
        }

        static public List<MedItem> GetExpiryMedItems
        {
            get
            {
                List<MedItem> list = new List<MedItem>();
                foreach (MedItem item in Service.GetAllItems())
                {
                    if (item.ExpiryDate < DateTime.Today) list.Add(item);
                }
                return list;
            }
        }

        static public void DeleteItems(List<MedItem> exp)
        {
            bool flag = true;
            foreach (MedItem itemE in exp)
            {
                if (flag)
                {
                    foreach (MedItem item in Service.GetAllItems())
                    {
                        if (item == itemE) { Service.RemoveProduct(item); flag = false; }

                    }
                }
                flag = true;
            }
        }

        static public List<Medicament> SmallAmmountItems
        {
            get
            {
                List<Medicament> list = new List<Medicament>();
                foreach (MedItem item in Service.GetAllItems())
                {
                    if (item is Medicament)
                    {
                        if (item.Ammount < 5)
                            list.Add(item as Medicament);
                    }
                }
                return list;

            }
        }

        static public List<Medicament> GetMedicaments
        {
            get
            {
                List<Medicament> list = new List<Medicament>();
                foreach (MedItem med in Service.GetAllItems())
                {
                    if (med is Medicament) list.Add(med as Medicament);
                }
                return list;
            }
        }

        static public List<User> GetUsers
        {
            get
            {
                return user.GetUsers;
            }
        }


        static public User GetUser(string name)
        {
            return user.GetUser(name);
        }

        static public MedicineMeal GetMedFromUser(string name, string pharm)
        {
            foreach (MedicineMeal med in Service.GetUser(name).PillsMeals)
            {
                if (med.Pill.Name == pharm) return med;

            }
            return null;
        }

        static public void RemoveUser(string name)
        {
            Service.GetUsers.Remove(Service.GetUser(name));
        }

        static public void AddUser(User user)
        {
            Service.GetUsers.Add(user);
        }

        static public List<Medicament> UsersMedicaments()
        {
            List<Medicament> list = new List<Medicament>();
            foreach (User user in Service.GetUsers)
            {
                foreach (MedicineMeal med in user.PillsMeals)
                {
                    list.Add(med.Pill);
                }
            }
            return list;
        }

        static public void AddMedMeal(string name, MedicineMeal pharm)
        {
            foreach (User user in Service.GetUsers)
            {
                if (user.Name == name) user.PillsMeals.Add(pharm);

            }

        }

        static public void ChangeAmmount(string usName, string item, int ammount)
        {
            foreach(User user in Service.GetUsers)
            {
                foreach(MedicineMeal med in user.PillsMeals)
                {
                    if (med.Pill.Name == item) med.Ammount = ammount;
                }
            }
        }

        static public void SaveUsers()
        {
            user.Save(user.GetUsers);
        }

        static public void SavePharmacy()
        {
            category.SaveCategories(category.GetCategories);
        }

    }
}
