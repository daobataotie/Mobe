//------------------------------------------------------------------------------
//
// file name：IInvoiceFTAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceFT
    /// </summary>
    public partial interface IInvoiceFTAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceFT> Select(DateTime start, DateTime end);

        IList<Model.InvoiceFT> Select(Helper.InvoiceStatus status);
    }
}

