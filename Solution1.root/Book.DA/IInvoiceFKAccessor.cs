//------------------------------------------------------------------------------
//
// file name：IInvoiceFKAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceFK
    /// </summary>
    public partial interface IInvoiceFKAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceFK> Select(DateTime start, DateTime end);

        IList<Model.InvoiceFK> Select(Helper.InvoiceStatus status);
    }
}

