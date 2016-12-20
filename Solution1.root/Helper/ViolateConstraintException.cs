using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class ViolateConstraintException : System.ApplicationException 
    {
        public ViolateConstraintException(string message): base(message)
        {
        }

        public ViolateConstraintException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
