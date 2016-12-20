using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// 单据状态
    /// </summary>
    [Flags]
    public enum InvoiceStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft,

        /// <summary>
        /// 正常的
        /// </summary>
        Normal,

        /// <summary>
        /// 作废的
        /// </summary>
        Null
    }
}
