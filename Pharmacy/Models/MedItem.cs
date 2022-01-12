using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pharmacy.Models
{
    [XmlInclude(typeof(Medicament)), XmlInclude(typeof(InstumentForMedicine))]
    public abstract class MedItem : ModelObject
    {

        protected override void SetRules()
        {
            Rules = new Dictionary<string, ModelRule>
            {
                { "NameItemNull", new ModelRule(obj =>!(string.IsNullOrEmpty(Name)), "Empty name!!!") },
                { "NameItemLength", new ModelRule(obj =>(Name.Length<35), "Incorrect length!!") },
                { "ManufactureItemNull", new ModelRule(obj =>!(string.IsNullOrEmpty(Manufacture)), "Empty name!!!") },
                { "ManufactureItemLength", new ModelRule(obj =>(Manufacture.Length<35), "Incorrect length!!") },
                { "WhatForItemNull", new ModelRule(obj =>!(string.IsNullOrEmpty(WhatFor)), "Empty name!!!") },
                { "WhatForItemLength", new ModelRule(obj =>(WhatFor.Length<35), "Incorrect length!!") },
                { "AmmountRule", new ModelRule(obj =>(Ammount>=0 && Ammount<10000), "Incorrect ammount!!!") },
                { "ExpiryDate", new ModelRule(obj =>(ExpiryDate.Year>2010), "Incorrect year!!") },
                { "ReleaseDate", new ModelRule(obj =>(ReleaseDate.Year>2010), "Incorrect year!!") },
                { "CompareDates", new ModelRule(obj =>(ExpiryDate>ReleaseDate), "Incorrect dates!!") }
                

            };
        }

      

        public MedItem(string name, DateTime releaseDate, DateTime expiryDate, string manufacture, string whatFor, int ammount, string link) : base()
        {
            SetRules();
            Name = name;
            ReleaseDate = releaseDate;
            ExpiryDate = expiryDate;
            Manufacture = manufacture;
            WhatFor = whatFor;
            Ammount = ammount;
            Link = link;
            ExpiryDate.ToString();
            
        }

        public MedItem()
        {
            SetRules();
        }

        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Manufacture { get; set; }
        public string WhatFor { get; set; }
        public int Ammount { get; set; }
        public string Link { get; set; }

        public bool CheckExpiryDate()
        {
            if (ExpiryDate > DateTime.Today)
                return false;
            else return true;
        }

        public void ChangeAmmount(int dig)
        {
            Ammount -= dig;
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj.GetType(), this.GetType()))
            {
                MedItem item = obj as MedItem;
                if (this.Name != item.Name) return false;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
