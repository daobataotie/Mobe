//------------------------------------------------------------------------------
//
// file name：IPCImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-15 13:56:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCImpactCheck
    /// </summary>
    public partial interface IPCImpactCheckAccessor : IAccessor
    {
        IList<Book.Model.PCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId);
    }
}

