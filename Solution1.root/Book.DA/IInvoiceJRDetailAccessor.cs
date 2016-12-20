//------------------------------------------------------------------------------
//
// file name：IInvoiceJRDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceJRDetail
    /// </summary>
    public partial interface IInvoiceJRDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceJRDetail> Select(Book.Model.InvoiceJR invoice);

        void Delete(Model.InvoiceJR invoice);

        IList<Book.Model.InvoiceJRDetail> Select(Model.Supplier supper);

        IList<Model.InvoiceJRDetail> Select(Model.InvoiceHC invoice);
    }
}

