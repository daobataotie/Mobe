//------------------------------------------------------------------------------
//
// file name：IInvoiceHRDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceHRDetail
    /// </summary>
    public partial interface IInvoiceHRDetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceHRDetail> Select(Book.Model.InvoiceHR invoice);

        void Delete(Model.InvoiceHR invoice);
    }
}

