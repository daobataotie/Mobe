//------------------------------------------------------------------------------
//
// file name：IInvoiceQODetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceQODetail
    /// </summary>
    public partial interface IInvoiceQODetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceQODetail> Select(Book.Model.InvoiceQO invoiceQO);

        void Delete(Model.InvoiceQO invoice);
    }
}

