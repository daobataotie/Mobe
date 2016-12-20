//------------------------------------------------------------------------------
//
// file name：IInvoicePIDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoicePIDetail
    /// </summary>
    public partial interface IInvoicePIDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoicePIDetail> Select(Book.Model.InvoicePI invoice);

        void Delete(Model.InvoicePI invoice);
    }
}

