//------------------------------------------------------------------------------
//
// file name：IPCOpticsCheckAccessor.cs
// author: mayanjun
// create date：2012-3-16 17:41:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCOpticsCheck
    /// </summary>
    public partial interface IPCOpticsCheckAccessor : IAccessor
    {
        IList<Model.PCOpticsCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId);

    }
}

