//------------------------------------------------------------------------------
//
// file name：IInvoiceZZAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceZZ
    /// </summary>
    public partial interface IInvoiceZZAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceZZ> Select(DateTime start, DateTime end);

        IList<Model.InvoiceZZ> Select(Helper.InvoiceStatus status);
    }
}

