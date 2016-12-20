using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    /// <summary>
    /// 此次操作资料,版本已被修改.
    /// </summary>
    public class VersionOverTimeException : System.ApplicationException
    {
        public VersionOverTimeException()
            : base()
        { }
        public VersionOverTimeException(string message)
            : base(message)
        { }

        public VersionOverTimeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
