//------------------------------------------------------------------------------
//
// file name：IInvoiceBSDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceBSDetail
    /// </summary>
    public partial interface IInvoiceBSDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceBSDetail> Select(Book.Model.InvoiceBS invoiceBS);
        void Delete(Model.InvoiceBS invoice);
    }
}

