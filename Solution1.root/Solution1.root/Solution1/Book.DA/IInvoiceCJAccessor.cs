//------------------------------------------------------------------------------
//
// file name：IInvoiceCJAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceCJ
    /// </summary>
    public partial interface IInvoiceCJAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceCJ> Select(DateTime start, DateTime end);

        IList<Model.InvoiceCJ> Select(Helper.InvoiceStatus status);

    }
}

