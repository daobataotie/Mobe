using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    [Flags]
    public enum CompanyKind
    {
        /// <summary>
        /// ��Ӧ��
        /// </summary>
        Supplier = 1,

        /// <summary>
        /// �ͻ�
        /// </summary>
        Customer
    }
}
