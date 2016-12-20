//------------------------------------------------------------------------------
//
// file name：IPCFinishCheckAccessor.cs
// author: mayanjun
// create date：2011-11-12 15:10:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCFinishCheck
    /// </summary>
    public partial interface IPCFinishCheckAccessor : IAccessor
    {
        IList<Model.PCFinishCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId);
    }
}

