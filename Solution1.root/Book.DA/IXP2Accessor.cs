//------------------------------------------------------------------------------
//
// file name：IXP2Accessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.XP2
    /// </summary>
    public partial interface IXP2Accessor : IEntityAccessor
    {
        /// <summary>
        /// 获取指定付款单的冲销记录
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        IList<Model.XP2> Select(Model.InvoiceFK invoice);

        /// <summary>
        /// 删除指定付款单的冲销记录
        /// </summary>
        /// <param name="invoice"></param>
        void Delete(Model.InvoiceFK invoice);
        
    }
}

