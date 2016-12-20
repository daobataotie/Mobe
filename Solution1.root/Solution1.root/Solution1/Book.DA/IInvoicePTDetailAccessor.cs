//------------------------------------------------------------------------------
//
// file name：IInvoicePTDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoicePTDetail
    /// </summary>
    public partial interface IInvoicePTDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoicePTDetail> Select(Book.Model.InvoicePT invoicePT);

        void Delete(Model.InvoicePT invoice);
    }
}

