using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;

namespace Pharmacy.Models
{
    public class MedicineMeal : ModelObject
    {
        public int Ammount { get; set; }
        public Medicament Pill { get; set; }

        private string _str = string.Empty;

        public string NameAndAmmountStr
        {
            get
            {
                _str = $"{Pill.Name} : {Ammount} в день";
                return _str;
            }
            set
            {
                _str = value;
            }
        }

        protected override void SetRules()
        {
            Rules = new Dictionary<string, ModelRule>
            {
                { "Ammount", new ModelRule(obj => ( Ammount>=0 && Ammount<1000), "Incorrect ammount!!!") },
                { "Pill", new ModelRule(obj =>( Service.GetMedicaments.Contains(Pill)), "You have not such medicament!!!") }

            };
        }
        public MedicineMeal()
        {
            SetRules();
            Pill = new Medicament();
        }

        public MedicineMeal(int amm, Medicament med) : this()
        {
            Ammount = amm;
            Pill = med;
        }
    }
}
