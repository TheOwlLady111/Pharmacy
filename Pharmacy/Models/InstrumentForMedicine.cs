using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class InstumentForMedicine : MedItem
    {
        public string Type { get; set; }

       protected override void SetRules()
        {
            base.SetRules();
            Rules.Add("TypeNull", new ModelRule(obj => !(string.IsNullOrEmpty(Name)), "Empty type!!!"));
            Rules.Add("TypeLength", new ModelRule(obj => (Name.Length < 35), "Incorrect length!!"));
        }
        public InstumentForMedicine() : base()
        {
            SetRules();
        }

        public InstumentForMedicine(string name, DateTime releaseDate, DateTime expiryDate, string manufacture, string whatFor, int ammount, string link, string type) : base(name, releaseDate, expiryDate, manufacture, whatFor, ammount, link)
        {
            SetRules();
            Type = type;
        }
    }
}
