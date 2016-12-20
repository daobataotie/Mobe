//------------------------------------------------------------------------------
//
// file name：IInvoicePackingDetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoicePackingDetail
    /// </summary>
    public partial interface IInvoicePackingDetailAccessor : IAccessor
    {
        IList<Model.InvoicePackingDetail> SelectByInvoicePackingId(string InvoicePackingId);

        void DeleteByInvoicePackingId(string InvoicePackingId);
    }
}

