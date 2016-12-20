//------------------------------------------------------------------------------
//
// file name：IInvoiceBYDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceBYDetail
    /// </summary>
    public partial interface IInvoiceBYDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceBYDetail> Select(Book.Model.InvoiceBY invoice);
        void Delete(Model.InvoiceBY invoice);
    }
}

