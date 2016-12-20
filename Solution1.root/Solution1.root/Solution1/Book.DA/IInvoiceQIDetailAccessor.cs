//------------------------------------------------------------------------------
//
// file name：IInvoiceQIDetailAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceQIDetail
    /// </summary>
    public partial interface IInvoiceQIDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceQIDetail> Select(Book.Model.InvoiceQI invoiceQI);

        void Delete(Model.InvoiceQI invoice);
    }
}

