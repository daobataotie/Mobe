//------------------------------------------------------------------------------
//
// file name：IInvoiceHCDetailAccessor.cs
// author: peidun
// create date：2008-11-29 12:15:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceHCDetail
    /// </summary>
    public partial interface IInvoiceHCDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceHCDetail> Select(Book.Model.InvoiceHC invoice);

        void Delete(Model.InvoiceHC invoice);
    }
}

