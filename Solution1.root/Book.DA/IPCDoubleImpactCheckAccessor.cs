//------------------------------------------------------------------------------
//
// file name：IPCDoubleImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-24 17:38:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCDoubleImpactCheck
    /// </summary>
    public partial interface IPCDoubleImpactCheckAccessor : IAccessor
    {
        IList<Model.PCDoubleImpactCheck> SelectByDateRage(DateTime startdate, DateTime enddate, int pcFlag, Model.Product product, Model.Customer customer, string CusXOId);
    }
}

