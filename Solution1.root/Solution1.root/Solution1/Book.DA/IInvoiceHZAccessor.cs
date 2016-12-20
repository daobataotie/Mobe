//------------------------------------------------------------------------------
//
// file name：IInvoiceHZAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceHZ
    /// </summary>
    public partial interface IInvoiceHZAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceHZ> Select(DateTime start, DateTime end);

        IList<Model.InvoiceHZ> Select(Helper.InvoiceStatus status);
    }
}

