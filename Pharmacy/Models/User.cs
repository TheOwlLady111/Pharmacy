using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Helpers;

namespace Pharmacy.Models
{
    public class User : ModelObject
    {
        public string Name { get; set; }
        public List<MedicineMeal> PillsMeals{get; set;}

        protected override void SetRules()
        {
            Rules = new Dictionary<string, ModelRule>
            {
                { "NameUserNull", new ModelRule(obj =>!(string.IsNullOrEmpty(Name)), "Empty user name!!!") },
                { "NameUserLength", new ModelRule(obj =>(Name.Length<35), "Too long user name!!!") }
            };
        }

        public User()
        {
            SetRules();
            PillsMeals = new List<MedicineMeal>();
        }

        public User(string str, List<MedicineMeal> meals) : this()
        {
            Name = str;
            PillsMeals = meals;
        }


    }
}
