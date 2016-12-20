using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class MessageValueException: System.ApplicationException
    {
        public MessageValueException()
            : base()
        {
        }
        public MessageValueException(string message)
            : base(message)
        {
        }
    }
}