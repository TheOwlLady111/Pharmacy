using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class Medicament : MedItem
    {
        public double Doze { set; get; }

        protected override void SetRules()
        {
            base.SetRules();
            this.Rules.Add("DozeRule", new ModelRule(obj => (Doze > 0 && Doze < 10000), "Incorrect ammount!!!"));
        }
        public Medicament() : base()
        {
            SetRules();
        }

        public Medicament(string name, DateTime releaseDate, DateTime expiryDate, string manufacture, string whatFor, int ammount, string link, double doze) : base(name, releaseDate, expiryDate, manufacture, whatFor, ammount, link)
        {
            SetRules();
            Doze = doze;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
