using System;
using System.Collections.Generic;
using System.Text;
namespace Helper
{
    public enum ProductType
    {
        /// <summary>
        /// 自製
        /// </summary>
        HomeMade = 0,
        /// <summary>
        /// 外購
        /// </summary>
        OutSourcing = 1,
        /// <summary>
        /// 耗用
        /// </summary>
        Consume = 2,
        /// <summary>
        /// 委外
        /// </summary>
        TrustOut = 3,
        /// <summary>
        ///自制(组装)
        /// </summary>
        Package = 4,
         /// <summary>
        ///自制(半成品加工)
        /// </summary>
        HomeMadeProcee = 5,
           /// <summary>
        ///委外(半成品加工)
        /// </summary>
        TrustOutProcee = 6
        

    }
}
