//------------------------------------------------------------------------------
//
// file name：IInvoiceJRAccessor.cs
// author: peidun
// create date：2008-11-29 12:15:14
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceJR
    /// </summary>
    public partial interface IInvoiceJRAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceJR> Select(DateTime start, DateTime end);

        IList<Model.InvoiceJR> Select(Helper.InvoiceStatus status);

        IList<Model.InvoiceJR> Select(Model.InvoiceJR invoicejr);

        IList<Model.InvoiceJR> Select(DateTime startdate, DateTime enddate, Model.Supplier supplier);
    }
}

