using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public enum InvoiceAudit
    {

        /// <summary>
        /// 0不运行电子审核
        /// </summary>
        NoUsing,
        /// <summary>
        /// 1待审核
        /// </summary>
        WaitAudit,
        /// <summary>
        /// 2审核中
        /// </summary>
        OnAuditing,
          /// <summary>
        /// 3已审核
        /// </summary>
        Audited,
           /// <summary>
        /// 4棄核
        /// </summary>
        GiveUpAudited
    }
    
}
