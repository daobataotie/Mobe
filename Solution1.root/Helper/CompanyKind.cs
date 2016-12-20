using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    [Flags]
    public enum CompanyKind
    {
        /// <summary>
        /// 供应商
        /// </summary>
        Supplier = 1,

        /// <summary>
        /// 客户
        /// </summary>
        Customer
    }
}
