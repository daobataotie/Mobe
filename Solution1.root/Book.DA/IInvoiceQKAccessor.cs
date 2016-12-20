//------------------------------------------------------------------------------
//
// file name：IInvoiceQKAccessor.cs
// author: peidun
// create date：2008/7/28 11:05:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceQK
    /// </summary>
    public partial interface IInvoiceQKAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceQK> Select(DateTime datetime1, DateTime datetime2);
        IList<Model.InvoiceQK> Select(Helper.InvoiceStatus status);
    }
}

