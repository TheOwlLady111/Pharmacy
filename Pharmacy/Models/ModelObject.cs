using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
     abstract public class ModelObject
    {
        protected Dictionary<string, ModelRule> Rules;

        protected abstract void SetRules();
        public bool IsValid
        {
            get
            {
                if (Validate()) return true;
                return false;
            }
        }

        public bool Validate()
        {
            if (Rules.Count > 0)
            {
                foreach (ModelRule rule in Rules.Values)
                {
                    if (!rule.propertyOfError(this))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Validate(string name)
        {
            if (!Rules[name].propertyOfError(this))
            {
                return false;
            }
            else
            { 
                return true; 
            }
        }

        public bool Validate(string name, ref string error)
        {
            if (!Rules[name].propertyOfError(this))
            {
                error = Rules[name].errorMessage;
                return false;
            }
            else
            { 
                return true; 
            }
        }
    }
}
