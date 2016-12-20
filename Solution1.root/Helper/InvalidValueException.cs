using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class InvalidValueException : System.ApplicationException
    {
        public InvalidValueException()
            : base()
        {
        }
        public InvalidValueException(string message)
            : base(message)
        {
        }
    }
}
