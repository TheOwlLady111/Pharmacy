using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class ModelRule
    {
        public Predicate<ModelObject> propertyOfError;
        public string errorMessage;

        public ModelRule(Predicate<ModelObject> property, string error)
        {
            propertyOfError = property;
            errorMessage = error;
        }
    }
}
