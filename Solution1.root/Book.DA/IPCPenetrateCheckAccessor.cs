//------------------------------------------------------------------------------
//
// file name：IPCPenetrateCheckAccessor.cs
// author: mayanjun
// create date：2012-3-21 11:02:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCPenetrateCheck
    /// </summary>
    public partial interface IPCPenetrateCheckAccessor : IAccessor
    {
        IList<Model.PCPenetrateCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId);
    }
}

