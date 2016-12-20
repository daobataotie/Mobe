//------------------------------------------------------------------------------
//
// file name：IInvoiceZSAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceZS
    /// </summary>
    public partial interface IInvoiceZSAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceZS> Select(DateTime start, DateTime end);

        IList<Model.InvoiceZS> Select(Helper.InvoiceStatus status);
    }
}

