//------------------------------------------------------------------------------
//
// file name：IInvoiceQIAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceQI
    /// </summary>
    public partial interface IInvoiceQIAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceQI> Select(DateTime start, DateTime end);

        IList<Model.InvoiceQI> Select(Helper.InvoiceStatus status);
    }
}

