using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class RequireValueException : System.ApplicationException
    {
        public RequireValueException() 
            : base()
        {
        }

        public RequireValueException(string message) 
            : base(message)
        {
            
        }


    }
}
