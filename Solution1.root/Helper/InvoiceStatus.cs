using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// ����״̬
    /// </summary>
    [Flags]
    public enum InvoiceStatus
    {
        /// <summary>
        /// �ݸ�
        /// </summary>
        Draft,

        /// <summary>
        /// ������
        /// </summary>
        Normal,

        /// <summary>
        /// ���ϵ�
        /// </summary>
        Null
    }
}
