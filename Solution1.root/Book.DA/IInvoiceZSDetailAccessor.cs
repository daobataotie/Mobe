//------------------------------------------------------------------------------
//
// file name：IInvoiceZSDetailAccessor.cs
// author: peidun
// create date：2008/6/20 15:51:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceZSDetail
    /// </summary>
    public partial interface IInvoiceZSDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceZSDetail> Select(Book.Model.InvoiceZS invoice);

        void Delete(Model.InvoiceZS invoice);
    }
}

