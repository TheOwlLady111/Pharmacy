using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    [Serializable]
    public class Category : ModelObject
    {
        public string Name { get; set; }
        public List<MedItem> Pharmacy {  get; set; }

        public Category(string name) : this()
        {
            Name = name;
        }

        protected override void SetRules()
        {
            Rules = new Dictionary<string, ModelRule>
            {
                { "NameNull", new ModelRule(obj =>!(string.IsNullOrEmpty(Name)), "Empty category name!!!") },
                { "NameLength", new ModelRule(obj =>( Name.Length<=35), "Incorrect length!!") }

            };
        }
        public Category()
        {
            SetRules();
        }

        public void Add(MedItem item)
        {
            Pharmacy.Add(item);
        }

        public void Remove(MedItem item)
        {
            Pharmacy.Remove(item);
        }


        public override string ToString()
        {
            return Name.ToString();
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj.GetType(), this.GetType()))
            {
                Category item = obj as Category;
                if (this.Name != item.Name) return false;
                return true;
            }
            return false;
        }
    }
}
