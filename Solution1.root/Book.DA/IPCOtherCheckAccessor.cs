//------------------------------------------------------------------------------
//
// file name：IPCOtherCheckAccessor.cs
// author: mayanjun
// create date：2011-11-10 15:05:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCOtherCheck
    /// </summary>
    public partial interface IPCOtherCheckAccessor : IAccessor
    {
        IList<Model.PCOtherCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId);
    }
}

