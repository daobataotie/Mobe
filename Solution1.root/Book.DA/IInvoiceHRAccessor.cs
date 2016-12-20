//------------------------------------------------------------------------------
//
// file name：IInvoiceHRAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceHR
    /// </summary>
    public partial interface IInvoiceHRAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceHR> Select(DateTime start, DateTime end);

        IList<Model.InvoiceHR> Select(Helper.InvoiceStatus status);
    }
}

