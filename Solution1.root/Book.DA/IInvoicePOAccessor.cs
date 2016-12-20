//------------------------------------------------------------------------------
//
// file name：IInvoicePOAccessor.cs
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
    /// Interface of data accessor of dbo.InvoicePO
    /// </summary>
    public partial interface IInvoicePOAccessor : IInvoiceAccessor
    {
        IList<Model.InvoicePO> Select(DateTime start, DateTime end);

        IList<Model.InvoicePO> Select(Helper.InvoiceStatus status);
    }
}

