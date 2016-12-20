//------------------------------------------------------------------------------
//
// file name：IInvoiceZGAccessor.cs
// author: mayanjun
// create date：2012-11-19 14:13:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceZG
    /// </summary>
    public partial interface IInvoiceZGAccessor : IAccessor
    {
        IList<Model.InvoiceZG> SelectInvoiceZG(DateTime StartDate, DateTime EndDate, Model.Customer XOcustomer, string InvoiceId, string ShippedId);
    }
}

