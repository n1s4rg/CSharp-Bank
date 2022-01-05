using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pw2Bank_Samiuddin_Syed.classes
{
    class Validator
    {
        public static bool IsNullValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            return false;
        }
        public static bool IsDigit(string value)
        {
            if (Regex.IsMatch(value, "^\\d*\\.?\\d*$"))
            {
                return true;
            }

            return false;
        }
    }
}
