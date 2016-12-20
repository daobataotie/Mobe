//------------------------------------------------------------------------------
//
// file name：IPCFogCheckAccessor.cs
// author: mayanjun
// create date：2012-3-16 17:42:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCFogCheck
    /// </summary>
    public partial interface IPCFogCheckAccessor : IAccessor
    {
        IList<Model.PCFogCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId);
        IList<Model.PCFogCheck> SelectByDate(DateTime startdate, DateTime enddate);
    }
}

