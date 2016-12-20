//------------------------------------------------------------------------------
//
// file name：IProductOnlineCheckAccessor.cs
// author: mayanjun
// create date：2013-3-25 17:50:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductOnlineCheck
    /// </summary>
    public partial interface IProductOnlineCheckAccessor : IAccessor
    {
        IList<Model.ProductOnlineCheck> SelectByDate(DateTime startDate, DateTime endDate, string InvoiceCusId);
    }
}

