//------------------------------------------------------------------------------
//
// file name：IPCBoxFootCheckAccessor.cs
// author: mayanjun
// create date：2013-1-28 15:42:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCBoxFootCheck
    /// </summary>
    public partial interface IPCBoxFootCheckAccessor : IAccessor
    {
        IList<Model.PCBoxFootCheck> SelectByRage(DateTime StartDate, DateTime EndDate, string InvoiceXOId, string PronoteHeaderId, Model.Product product);
    }
}

