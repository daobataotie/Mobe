//------------------------------------------------------------------------------
//
// file name：IXR2Accessor.cs
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
    /// Interface of data accessor of dbo.XR2
    /// </summary>
    public partial interface IXR2Accessor : IEntityAccessor
    {
        /// <summary>
        /// 获取指定收款单的冲销记录
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        IList<Model.XR2> Select(Model.InvoiceSK invoice);

        /// <summary>
        /// 删除指定收款单的冲销记录
        /// </summary>
        /// <param name="invoice"></param>
        void Delete(Model.InvoiceSK invoice);
    }
}

