//------------------------------------------------------------------------------
//
// file name：IInvoicePackingAccessor.cs
// author: mayanjun
// create date：2013-1-14 10:58:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoicePacking
    /// </summary>
    public partial interface IInvoicePackingAccessor : IAccessor
    {
        IList<Model.InvoicePacking> SelectByCondition(DateTime startDate, DateTime endDate, string No, string InvoiceOf, string ShippedBy, string Consignee);

        string SelectCustomerInvoiceId(string id);
    }
}

