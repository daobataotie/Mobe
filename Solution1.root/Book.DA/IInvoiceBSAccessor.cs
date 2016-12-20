//------------------------------------------------------------------------------
//
// file name：IInvoiceBSAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceBS
    /// </summary>
    public partial interface IInvoiceBSAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceBS> Select(DateTime start, DateTime end);

        IList<Model.InvoiceBS> Select(Helper.InvoiceStatus status);
    }
}

