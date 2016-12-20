//------------------------------------------------------------------------------
//
// file name：IInvoiceXJAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceXJ
    /// </summary>
    public partial interface IInvoiceXJAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceXJ> Select(DateTime start, DateTime end);

        IList<Model.InvoiceXJ> Select(Helper.InvoiceStatus status);

        IList<Model.InvoiceXJ> Select(DateTime startDate, DateTime endDate, string customerid, string productid, string invoicexjid, string companyid);
    }
}

