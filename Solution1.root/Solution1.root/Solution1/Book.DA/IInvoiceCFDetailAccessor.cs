//------------------------------------------------------------------------------
//
// file name：IInvoiceCFDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceCFDetail
    /// </summary>
    public partial interface IInvoiceCFDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceCFDetail> Select(Book.Model.InvoiceCF invoice);

        void Delete(Model.InvoiceCF invoice);

        IList<Model.InvoiceCFDetail> Select(string kind, Model.InvoiceCF invoice);
    }
}

