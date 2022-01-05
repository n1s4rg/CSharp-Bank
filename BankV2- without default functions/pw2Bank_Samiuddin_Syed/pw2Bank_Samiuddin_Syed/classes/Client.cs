using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw2Bank_Samiuddin_Syed.classes
{
    class Client
    {
        private string givenName, familyName;

        public Client(string givenName, string familyName)
        {
            GivenName = givenName;
            FamilyName = familyName;
        } 

        public string GivenName { 
            get => givenName; 
            set
            {
                if (Validator.IsNullValue(value))
                {
                    throw new NullValueException("Given name can't be empty");
                }
                else
                {
                    givenName = value;
                }
            }
        }
        public string FamilyName { get => familyName; set
            {
                if (Validator.IsNullValue(value))
                {
                    throw new NullValueException("Family name can't be empty");
                }
                else
                {
                    familyName = value;
                }
            }
        }
        
    }
}
