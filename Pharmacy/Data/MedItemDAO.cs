using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class MedItemDAO
    {
        private MedItem item;
        private Db data;
        private List<Category> categories;
        public MedItemDAO()
        {
            data = new Db();
            categories = new List<Category>();
            data = data.GetInstance();
            categories = data.GetCategories;
        }
        public MedItem GetMedItem(string name)
        {
            foreach (Category cat in categories)
            {
                foreach (MedItem item in cat.Pharmacy)
                    if (item.Name == name) return item;
            }
            return null;
        }

        public MedItem GetMedItem(string nameCat, string nameMed)
        {
            foreach (Category cat in categories)
            {
                if (cat.Name == nameCat)
                {
                    foreach (MedItem item in cat.Pharmacy)
                    {
                        if (item.Name == nameCat) return item;
                    }
                }
            }

            return null;
        }

        public void RemoveItem(string name)
        {
            foreach (Category cat in categories)
            {
                for (int i=0; i<cat.Pharmacy.Count; i++)
                    if (cat.Pharmacy[i].Name == name)
                    {
                        cat.Remove(cat.Pharmacy[0]);
                        i--;
                    }
            }
        }

    }
}
