//------------------------------------------------------------------------------
//
// file name：IInvoiceSKAccessor.cs
// author: peidun
// create date：2008/6/6 14:48:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceSK
    /// </summary>
    public partial interface IInvoiceSKAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceSK> Select(DateTime start, DateTime end);

        IList<Model.InvoiceSK> Select(Helper.InvoiceStatus status);
    }
}

