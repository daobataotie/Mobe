using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public enum InvoiceFlag
    {
        /// <summary>
        /// 等候处理中
        /// </summary>
        WaitingForProcess,
        /// <summary>
        /// 处理中，未完成
        /// </summary>
        Processing,
        /// <summary>
        /// 货物已到达
        /// </summary>
        Processed
    }

    public enum DetailsFlag 
    {
        /// <summary>
        /// 在路上
        /// </summary>
        OnTheWay,
        /// <summary>
        /// 部分到达
        /// </summary>
        PartArrived,
        /// <summary>
        /// 全部到达
        /// </summary>
        AllArrived        
    }
  
}
