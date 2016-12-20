//------------------------------------------------------------------------------
//
// file name：IInvoiceJCAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceJC
    /// </summary>
    public partial interface IInvoiceJCAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceJC> Select(DateTime start, DateTime end);

        IList<Model.InvoiceJC> Select(Helper.InvoiceStatus status);
    }
}

