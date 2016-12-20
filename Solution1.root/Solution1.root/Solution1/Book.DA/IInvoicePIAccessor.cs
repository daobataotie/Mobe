//------------------------------------------------------------------------------
//
// file name：IInvoicePIAccessor.cs
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
    /// Interface of data accessor of dbo.InvoicePI
    /// </summary>
    public partial interface IInvoicePIAccessor : IInvoiceAccessor
    {
        IList<Model.InvoicePI> Select(DateTime start, DateTime end);

        IList<Model.InvoicePI> Select(Helper.InvoiceStatus status);
    }
}

