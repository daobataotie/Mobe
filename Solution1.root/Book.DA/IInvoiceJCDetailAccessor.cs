//------------------------------------------------------------------------------
//
// file name：IInvoiceJCDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceJCDetail
    /// </summary>
    public partial interface IInvoiceJCDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceJCDetail> Select(Book.Model.InvoiceJC invoice);

        void Delete(Model.InvoiceJC invoice);

        IList<Book.Model.InvoiceJCDetail> Select(Model.Supplier supper);

        IList<Book.Model.InvoiceJCDetail> Select(Model.InvoiceHR invoice);
    }
}

