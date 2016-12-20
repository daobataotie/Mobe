//------------------------------------------------------------------------------
//
// file name：IInvoiceHZDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceHZDetail
    /// </summary>
    public partial interface IInvoiceHZDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceHZDetail> Select(Book.Model.InvoiceHZ invoice);

        void Delete(Model.InvoiceHZ invoice);
    }
}

