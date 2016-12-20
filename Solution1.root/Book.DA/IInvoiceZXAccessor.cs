//------------------------------------------------------------------------------
//
// file name：IInvoiceZXAccessor.cs
// author: mayanjun
// create date：2012-10-29 14:32:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceZX
    /// </summary>
    public partial interface IInvoiceZXAccessor : IAccessor
    {
        IList<Model.InvoiceZX> selectByPackingId(string PackingId);
        IList<Model.InvoiceZX> SelectPackingRecord(DateTime dateStart, DateTime dateEnd, Model.Customer customer, Model.Customer XOcustomer);
        int SelectHasPackingNum(string productId);
    }
}

