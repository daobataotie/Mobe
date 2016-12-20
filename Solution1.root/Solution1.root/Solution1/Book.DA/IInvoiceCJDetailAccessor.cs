//------------------------------------------------------------------------------
//
// file name：IInvoiceCJDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceCJDetail
    /// </summary>
    public partial interface IInvoiceCJDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceCJDetail> Select(Book.Model.InvoiceCJ invoiceCJ);
        void Delete(Model.InvoiceCJ invoice);
        
    }
}

