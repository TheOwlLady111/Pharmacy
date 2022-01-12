using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class CategoryDAO
    {
        private Db data;
        private List<Category> categories;
        private Category category;

        public CategoryDAO()
        {
            data = new Db();
            categories = new List<Category>();
            data = data.GetInstance();
            categories = data.GetCategories;

        }
        public List<Category> GetCategories
        {
            get
            {
                return categories;
            }
        }

        public void SaveCategories(List<Category> list)
        {
            data.SetData(list);
        }
        public Category GetCategory(string name)
        {
            foreach (Category cat in categories)
            {
                if (cat.Name == name) return cat;

            }
            return null;
        }

        public void Add(Category cat)
        {
            categories.Add(cat);
        }

        public void Remove(Category cat)
        {
            categories.Remove(cat);
        }

        public void AddMedItem(MedItem item)
        {
            category.Add(item);
        }

        public void RemoveProduct(MedItem item)
        {
            category.Remove(item);
        }

       


    }
}
