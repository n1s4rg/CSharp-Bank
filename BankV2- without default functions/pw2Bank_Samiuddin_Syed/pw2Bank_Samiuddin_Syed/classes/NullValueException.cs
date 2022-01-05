using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw2Bank_Samiuddin_Syed.classes
{
    public class NullValueException : Exception
    {
        private static string message = "Value can't be empty or null";
        public NullValueException() : base(message)
        {

        }
        public NullValueException(string exceptionMessage) : base(exceptionMessage)
        {

        }
    }
}
