//------------------------------------------------------------------------------
//
// file name：IInvoiceQOAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceQO
    /// </summary>
    public partial interface IInvoiceQOAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceQO> Select(DateTime start, DateTime end);

        IList<Model.InvoiceQO> Select(Helper.InvoiceStatus status);
    }
}

