//------------------------------------------------------------------------------
//
// file name：IInvoicePTAccessor.cs
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
    /// Interface of data accessor of dbo.InvoicePT
    /// </summary>
    public partial interface IInvoicePTAccessor : IInvoiceAccessor
    {
        IList<Model.InvoicePT> Select(DateTime startTime, DateTime endTime, string invoiceId, string employeeId, string depot, string depotIn,string productId);

        IList<Model.InvoicePT> Select(Helper.InvoiceStatus status);

        IList<Model.InvoicePT> Select(DateTime startTime, DateTime endTime);
        
    }
}

