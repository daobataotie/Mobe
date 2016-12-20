//------------------------------------------------------------------------------
//
// file name：IInvoiceCFAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceCF
    /// </summary>
    public partial interface IInvoiceCFAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceCF> Select(DateTime start, DateTime end);

        IList<Model.InvoiceCF> Select(Helper.InvoiceStatus status);
    }
}

