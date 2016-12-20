//------------------------------------------------------------------------------
//
// file name：IInvoiceZZDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceZZDetail
    /// </summary>
    public partial interface IInvoiceZZDetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceZZDetail> Select(string productKind, Model.InvoiceZZ invoiceZZ);

        IList<Book.Model.InvoiceZZDetail> Select(Book.Model.InvoiceZZ invoiceZZ);

        void Delete(Model.InvoiceZZ invoice);
    }
}

