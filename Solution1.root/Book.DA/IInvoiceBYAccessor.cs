//------------------------------------------------------------------------------
//
// file name：IInvoiceBYAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceBY
    /// </summary>
    public partial interface IInvoiceBYAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceBY> Select(DateTime start, DateTime end);

        IList<Model.InvoiceBY> Select(Helper.InvoiceStatus status);
    }
}

